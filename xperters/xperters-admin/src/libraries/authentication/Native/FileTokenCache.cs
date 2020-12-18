using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace Xperters.Authentication.Native
{
    internal class FileTokenCache : TokenCache
    {
        private readonly string _cacheFilePath;
        private static readonly object FileLock = new object();
        private const string fileName = "TokenCache.dat";

        // Initializes the cache against a local file.
        // If the file is already present, it loads its content in the ADAL cache
        public FileTokenCache(string env)
        {
            // Default to the current executing assembly's name
            // but try to get the executable name if possible
            var name = Assembly.GetExecutingAssembly().GetName().Name;
            try
            {
                name = Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location);
            }
            catch { /* ignore */ }
            name = string.IsNullOrWhiteSpace(env) ? name : $"{name}_{env}";
            var filePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                name);

            // Directory.CreateDirectory will only ever create a directory
            // if it doesn't exist so fine to call this multiple times
            Directory.CreateDirectory(filePath);

            _cacheFilePath = Path.Combine(filePath, fileName);
            AfterAccess = AfterAccessNotification;
            BeforeAccess = BeforeAccessNotification;
            lock (FileLock)
            {
                Deserialize(File.Exists(_cacheFilePath) ?
                    ProtectedData.Unprotect(File.ReadAllBytes(_cacheFilePath), null, DataProtectionScope.CurrentUser) :
                    null);
            }
        }

        // Empties the persistent store.
        public override void Clear()
        {
            base.Clear();
            lock (FileLock)
            {
                File.Delete(_cacheFilePath);
            }
        }

        // Triggered right before ADAL needs to access the cache.
        // Reload the cache from the persistent store in case it changed since the last access.
        void BeforeAccessNotification(TokenCacheNotificationArgs args)
        {
            lock (FileLock)
            {
                Deserialize(File.Exists(_cacheFilePath) ?
                    ProtectedData.Unprotect(File.ReadAllBytes(_cacheFilePath), null, DataProtectionScope.CurrentUser) :
                    null);
            }
        }

        // Triggered right after ADAL accessed the cache.
        void AfterAccessNotification(TokenCacheNotificationArgs args)
        {
            // if the access operation resulted in a cache update
            if (HasStateChanged)
            {
                lock (FileLock)
                {
                    // reflect changes in the persistent store
                    File.WriteAllBytes(_cacheFilePath, ProtectedData.Protect(Serialize(), null, DataProtectionScope.CurrentUser));
                    // once the write operation took place, restore the HasStateChanged bit to false
                    HasStateChanged = false;
                }
            }
        }
    }
}