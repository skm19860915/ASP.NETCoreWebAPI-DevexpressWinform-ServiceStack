using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Azure.KeyVault;
using Microsoft.Data.SqlClient;

namespace xperters.encryption
{
    public class SqlColumnEncryptionAzureKeyVaultProvider : SqlColumnEncryptionKeyStoreProvider
    {
        private readonly byte[] _firstVersion = {1};
        private KeyVaultClient KeyVaultClient { get; }
        public const string ProviderName = "AZURE_KEY_VAULT";

        public SqlColumnEncryptionAzureKeyVaultProvider(KeyVaultClient.AuthenticationCallback authenticationCallback)
        {
            if (authenticationCallback == null)
                throw new ArgumentNullException(nameof(authenticationCallback));
            KeyVaultClient = new KeyVaultClient(authenticationCallback);
        }

        public override byte[] DecryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] encryptedColumnEncryptionKey)
        {
            ValidateNonEmptyAkvPath(masterKeyPath, true);

            if (encryptedColumnEncryptionKey == null)
                throw new ArgumentNullException(nameof(encryptedColumnEncryptionKey), "Internal error. Encrypted column encryption key cannot be null.");

            if (encryptedColumnEncryptionKey.Length == 0)
                throw new ArgumentException("Internal error. Empty encrypted column encryption key specified.", nameof(encryptedColumnEncryptionKey));

            ValidateEncryptionAlgorithm(ref encryptionAlgorithm, true);

            int akvKeySize = GetAkvKeySize(masterKeyPath);
            if (encryptedColumnEncryptionKey[0] != _firstVersion[0])
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Specified encrypted column encryption key contains an invalid encryption algorithm version '{0}'. Expected version is '{1}'.", encryptedColumnEncryptionKey[0].ToString("X2"), _firstVersion[0].ToString("X2")), nameof(encryptedColumnEncryptionKey));

            int length1 = _firstVersion.Length;
            ushort uint161 = BitConverter.ToUInt16(encryptedColumnEncryptionKey, length1);
            int startIndex = length1 + 2;

            ushort uint162 = BitConverter.ToUInt16(encryptedColumnEncryptionKey, startIndex);

            int srcOffset1 = startIndex + 2 + uint161;
            if (uint162 != akvKeySize)
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The specified encrypted column encryption key's ciphertext length: {0} does not match the ciphertext length: {1} when using column master key (Azure Key Vault key) in '{2}'. The encrypted column encryption key may be corrupt, or the specified Azure Key Vault key path may be incorrect.", uint162, akvKeySize, masterKeyPath), nameof(encryptedColumnEncryptionKey));
            int length2 = encryptedColumnEncryptionKey.Length - srcOffset1 - uint162;

            if (length2 != akvKeySize)
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The specified encrypted column encryption key's signature length: {0} does not match the signature length: {1} when using column master key (Azure Key Vault key) in '{2}'. The encrypted column encryption key may be corrupt, or the specified Azure Key Vault key path may be incorrect.", length2, akvKeySize, masterKeyPath), nameof(encryptedColumnEncryptionKey));

            byte[] encryptedColumnEncryptionKey1 = new byte[uint162];

            Buffer.BlockCopy(encryptedColumnEncryptionKey, srcOffset1, encryptedColumnEncryptionKey1, 0, uint162);
            int srcOffset2 = srcOffset1 + uint162;
            byte[] signature = new byte[length2];

            Buffer.BlockCopy(encryptedColumnEncryptionKey, srcOffset2, signature, 0, signature.Length);
            byte[] hash;
            using (SHA256 shA256Cng = SHA256.Create())
            {
                shA256Cng.TransformFinalBlock(encryptedColumnEncryptionKey, 0, encryptedColumnEncryptionKey.Length - signature.Length);
                hash = shA256Cng.Hash;
            }

            if (hash == null)
                throw new CryptographicException("Hash should not be null while decrypting encrypted column encryption key.");

            if (!AzureKeyVaultVerifySignature(hash, signature, masterKeyPath))
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The specified encrypted column encryption key signature does not match the signature computed with the column master key (Asymmetric key in Azure Key Vault) in '{0}'. The encrypted column encryption key may be corrupt, or the specified path may be incorrect.", masterKeyPath), nameof(encryptedColumnEncryptionKey));

            return AzureKeyVaultUnWrap(masterKeyPath, encryptionAlgorithm, encryptedColumnEncryptionKey1);
        }

        public override byte[] EncryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] columnEncryptionKey)
        {
            ValidateNonEmptyAkvPath(masterKeyPath, false);
            if (columnEncryptionKey == null)
                throw new ArgumentNullException(nameof(columnEncryptionKey), "Column encryption key cannot be null.");

            if (columnEncryptionKey.Length == 0)
                throw new ArgumentException("Empty column encryption key specified.", nameof(columnEncryptionKey));

            ValidateEncryptionAlgorithm(ref encryptionAlgorithm, false);
            int akvKeySize = GetAkvKeySize(masterKeyPath);
            byte[] numArray1 = { _firstVersion[0] };
            byte[] bytes1 = Encoding.Unicode.GetBytes(masterKeyPath.ToLowerInvariant());
            byte[] bytes2 = BitConverter.GetBytes((short)bytes1.Length);
            byte[] inputBuffer = AzureKeyVaultWrap(masterKeyPath, encryptionAlgorithm, columnEncryptionKey);
            byte[] bytes3 = BitConverter.GetBytes((short)inputBuffer.Length);
            if (inputBuffer.Length != akvKeySize)
                throw new CryptographicException("cipherText length does not match the RSA key size");
            byte[] hash;
            using (var shA256Cng = SHA256.Create())
            {
                shA256Cng.TransformBlock(numArray1, 0, numArray1.Length, numArray1, 0);
                shA256Cng.TransformBlock(bytes2, 0, bytes2.Length, bytes2, 0);
                shA256Cng.TransformBlock(bytes3, 0, bytes3.Length, bytes3, 0);
                shA256Cng.TransformBlock(bytes1, 0, bytes1.Length, bytes1, 0);
                shA256Cng.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
                hash = shA256Cng.Hash;
            }
            byte[] signature = AzureKeyVaultSignHashedData(hash, masterKeyPath);
            if (signature.Length != akvKeySize)
                throw new CryptographicException("Signed hash length does not match the RSA key size");

            if (!AzureKeyVaultVerifySignature(hash, signature, masterKeyPath))
                throw new CryptographicException("Invalid signature of the encrypted column encryption key computed.");

            byte[] numArray2 = new byte[numArray1.Length + bytes3.Length + bytes2.Length + inputBuffer.Length + bytes1.Length + signature.Length];
            int dstOffset1 = 0;
            Buffer.BlockCopy(numArray1, 0, numArray2, dstOffset1, numArray1.Length);
            int dstOffset2 = dstOffset1 + numArray1.Length;
            Buffer.BlockCopy(bytes2, 0, numArray2, dstOffset2, bytes2.Length);
            int dstOffset3 = dstOffset2 + bytes2.Length;
            Buffer.BlockCopy(bytes3, 0, numArray2, dstOffset3, bytes3.Length);
            int dstOffset4 = dstOffset3 + bytes3.Length;
            Buffer.BlockCopy(bytes1, 0, numArray2, dstOffset4, bytes1.Length);
            int dstOffset5 = dstOffset4 + bytes1.Length;
            Buffer.BlockCopy(inputBuffer, 0, numArray2, dstOffset5, inputBuffer.Length);
            int dstOffset6 = dstOffset5 + inputBuffer.Length;
            Buffer.BlockCopy(signature, 0, numArray2, dstOffset6, signature.Length);
            return numArray2;
        }

        private void ValidateEncryptionAlgorithm(ref string encryptionAlgorithm, bool isSystemOp)
        {
            if (encryptionAlgorithm == null)
            {
                if (isSystemOp)
                    throw new ArgumentNullException(nameof(encryptionAlgorithm), "Internal error. Key encryption algorithm cannot be null.");

                throw new ArgumentNullException(nameof(encryptionAlgorithm), "Key encryption algorithm cannot be null.");
            }

            if (encryptionAlgorithm.Equals("RSA_OAEP", StringComparison.OrdinalIgnoreCase))
                encryptionAlgorithm = "RSA-OAEP";
            if (!string.Equals(encryptionAlgorithm, "RSA-OAEP", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid key encryption algorithm specified: '{0}'. Expected value: '{1}'.", encryptionAlgorithm, "RSA-OAEP"), nameof(encryptionAlgorithm));
        }

        private void ValidateNonEmptyAkvPath(string masterKeyPath, bool isSystemOp)
        {
            if (string.IsNullOrWhiteSpace(masterKeyPath))
            {
                string message = masterKeyPath == null ? "Azure Key Vault key path cannot be null." : string.Format(CultureInfo.InvariantCulture, "Invalid Azure Key Vault key path specified: '{0}'.", masterKeyPath);
                if (isSystemOp)
                    throw new ArgumentNullException(nameof(masterKeyPath), "Internal error.  " + message);

                throw new ArgumentException(message, nameof(masterKeyPath));
            }

            if (!Uri.TryCreate(masterKeyPath, UriKind.Absolute, out var result))
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid url specified: '{0}'.", masterKeyPath), nameof(masterKeyPath));

            if (!result.Host.ToLowerInvariant().EndsWith("vault.azure.net", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid Azure Key Vault key path specified: '{0}'.", masterKeyPath), nameof(masterKeyPath));
        }

        private byte[] AzureKeyVaultWrap(string masterKeyPath, string encryptionAlgorithm, byte[] columnEncryptionKey)
        {
            if (columnEncryptionKey == null)
                throw new ArgumentNullException(nameof(columnEncryptionKey));
            return KeyVaultClient.WrapKeyAsync(masterKeyPath, encryptionAlgorithm, columnEncryptionKey).GetAwaiter().GetResult().Result;
        }

        private byte[] AzureKeyVaultUnWrap(string masterKeyPath, string encryptionAlgorithm, byte[] encryptedColumnEncryptionKey)
        {
            if (encryptedColumnEncryptionKey == null)
                throw new ArgumentNullException(nameof(encryptedColumnEncryptionKey));

            if (encryptedColumnEncryptionKey.Length == 0)
                throw new ArgumentException("encryptedColumnEncryptionKey length should not be zero");

            return KeyVaultClient.UnwrapKeyAsync(masterKeyPath, encryptionAlgorithm, encryptedColumnEncryptionKey).GetAwaiter().GetResult().Result;
        }

        private byte[] AzureKeyVaultSignHashedData(byte[] dataToSign, string masterKeyPath)
        {
            return KeyVaultClient.SignAsync(masterKeyPath, "RS256", dataToSign).GetAwaiter().GetResult().Result;
        }

        private bool AzureKeyVaultVerifySignature(byte[] dataToVerify, byte[] signature, string masterKeyPath)
        {
            return KeyVaultClient.VerifyAsync(masterKeyPath, "RS256", dataToVerify, signature).GetAwaiter().GetResult();
        }

        private int GetAkvKeySize(string masterKeyPath)
        {
            var result = KeyVaultClient.GetKeyAsync(masterKeyPath).GetAwaiter().GetResult();
            if (!string.Equals(result.Key.Kty, "RSA", StringComparison.InvariantCultureIgnoreCase) && !string.Equals(result.Key.Kty, "RSA-HSM", StringComparison.InvariantCultureIgnoreCase))
                throw new Exception(string.Format(CultureInfo.InvariantCulture, "Cannot use a non-RSA key: '{0}'", result.Key.Kty));

            return result.Key.N.Length;
        }
    }
}
