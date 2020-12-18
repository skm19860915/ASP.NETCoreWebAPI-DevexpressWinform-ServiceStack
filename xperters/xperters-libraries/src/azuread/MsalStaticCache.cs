using System.Collections.Generic;
using System.Threading;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Http;


namespace xperters.azuread
{
    public class MsalStaticCache
    {
        private static readonly Dictionary<string, byte[]> StaticCache = new Dictionary<string, byte[]>();

        private static readonly ReaderWriterLockSlim SessionLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
        readonly string _cacheId;
        private HttpContext _httpContext;

        ITokenCache _cache;

        public MsalStaticCache(string userId, HttpContext httpContext)
        {
            // not object, we want the SUB
            _cacheId = userId + "_TokenCache";
            _httpContext = httpContext;
        }

        public ITokenCache EnablePersistence(ITokenCache cache)
        {
            this._cache = cache;
            cache.SetBeforeAccess(BeforeAccessNotification);
            cache.SetAfterAccess(AfterAccessNotification);
            Load();
            return cache;
        }

        public void Load()
        {
            SessionLock.EnterReadLock();
            byte[] blob = StaticCache.ContainsKey(_cacheId) ? StaticCache[_cacheId] : null;
            if (blob != null)
            {
                _cache.DeserializeMsalV3(blob);
            }
            SessionLock.ExitReadLock();
        }

        public void Persist()
        {
            SessionLock.EnterWriteLock();

            // Reflect changes in the persistent store
            StaticCache[_cacheId] = _cache.SerializeMsalV3();
            SessionLock.ExitWriteLock();
        }

        // Triggered right before MSAL needs to access the cache.
        // Reload the cache from the persistent store in case it changed since the last access.
        void BeforeAccessNotification(TokenCacheNotificationArgs args)
        {
            Load();
        }

        // Triggered right after MSAL accessed the cache.
        void AfterAccessNotification(TokenCacheNotificationArgs args)
        {
            // if the access operation resulted in a cache update
            if (args.HasStateChanged)
            {
                Persist();
            }
        }
    }
}