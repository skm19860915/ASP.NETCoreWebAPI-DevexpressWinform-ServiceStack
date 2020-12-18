using System;
using System.Security.Cryptography;
using System.Text;
using xperters.payments.Interfaces;

namespace xperters.payments.Signing
{
    public class SigningService : ISigningService
    {
        private const string Sha256 = "SHA256";
        private readonly RSACryptoServiceProvider _provider;

        public SigningService(string privateKey)
        {
            var privateKeyBytes = Helpers.GetBytesFromPEM(privateKey, PemStringType.RsaPrivateKey);
            _provider = Crypto.DecodeRsaPrivateKey(privateKeyBytes);
        }

        public string SignData(string data)
        {
            var dataBytes = Encoding.UTF8.GetBytes(data);
            var signedData = _provider.SignData(dataBytes, Sha256);
            var base64SignedData = Convert.ToBase64String(signedData);

            return base64SignedData;
        }

        public bool VerifyData(string expected, string signedBase64)
        {
            
            var expectedBytes = Encoding.UTF8.GetBytes(expected);

            var signature = Convert.FromBase64String(signedBase64);
            bool verified = _provider.VerifyData(expectedBytes, Sha256, signature);

            return verified;
        }
    }
}
