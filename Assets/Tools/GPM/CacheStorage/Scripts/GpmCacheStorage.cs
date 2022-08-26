using System;

namespace Gpm.CacheStorage
{
    public static class GpmCacheStorage
    {
        public const string NAME = "GpmCacheStorage";
        public const string VERSION = "0.2.1";

        public class Result
        {
            public bool success;
            public CacheInfo info;

            public byte[] data;
        }

        private static CacheStorageConfig cacheConfig;
        private static CachePackage cachePackage = new CachePackage();

        public static CacheStorageConfig Config
        {
            get
            {
                if (cacheConfig == null)
                {
                    LoadConfig();
                }

                return cacheConfig;
            }
        }

        public static CachePackage Package
        {
            get
            {
                if (cachePackage == null)
                {
                    LoadPackage();
                }

                return cachePackage;
            }
        }


        public static event System.Action onChangeCache;

        public static int GetCacheCount()
        {
            return Package.cacheStorage.Count;
        }

        public static long GetCacheSize()
        {
            return Package.cachedSize;
        }

        public static void ClearCache()
        {
            Package.RemoveAll();
        }

        public static void SetMaxCount(int count = 0, bool applyStorage = true)
        {
            Config.SetMaxCount(count);

            if (applyStorage == true &&
                count > 0)
            {
                Package.SecuringStorageCount();
            }
        }

        public static int GetMaxCount()
        {
            return Config.GetMaxCount();
        }

        public static void SetMaxSize(long size = 0, bool applyStorage = true)
        {
            Config.SetMaxSize(size);

            if (applyStorage == true &&
                size > 0)
            {
                Package.SecuringStorage(size);
            }
        }

        public static long GetMaxSize()
        {
            return Config.GetMaxSize();
        }


        public static void SetReRequestTime(long value)
        {
            Config.SetReRequestTime(value);
        }

        public static long GetReRequestTime()
        {
            return Config.GetReRequestTime();
        }

        static GpmCacheStorage()
        {
            initialize();
        }

        public static void SetCachePath(string path)
        {
            Config.SetCachePath(path);
        }

        public static string GetCachePath()
        {
            return Config.GetCachePath();
        }

        public static CacheInfo RequestHttpCache(string url, Action<Result> onResult)
        {
            return Package.Request(url, onResult);
        }

        public static CacheInfo RequestLocalCache(string url, Action<Result> onResult)
        {
            return Package.RequestLocal(url, onResult);
        }

        public static CacheInfo GetCachedTexture(string url, Action<CachedTexture> onResult)
        {
            CacheInfo info = Package.GetCacheInfo(url);
            if (info != null)
            {
                CachedTexture cachedTexture = CachedTextureManager.Get(info);
                if (cachedTexture != null)
                {
                    onResult(cachedTexture);
                    return info;
                }
            }

            return RequestLocalCache(url, (result) =>
            {
                if (result.success == true)
                {
                    onResult(CachedTextureManager.Cache(result.info, false, result.data));
                }
                else
                {
                    onResult(null);
                }
            });
        }

        public static CacheInfo RequestTexture(string url, Action<CachedTexture> onResult)
        {
            CacheInfo info = Package.GetCacheInfo(url);
            if (info != null)
            {
                CachedTexture cachedTexture = CachedTextureManager.Get(info);
                if (cachedTexture != null)
                {
                    if(cachedTexture.requested == true)
                    {
                        onResult(cachedTexture);
                        return info;
                    }
                }
            }

            info = RequestHttpCache(url, (result) =>
            {
                if (result.success == true)
                {
                    onResult(CachedTextureManager.Cache(result.info, true, result.data));
                }
                else
                {
                    onResult(null);
                }
            });

            return info;
        }

        internal static void initialize()
        {
            LoadConfig();
            LoadPackage();
        }

        internal static CacheStorageConfig LoadConfig()
        {
            cacheConfig = CacheStorageConfig.Load();
            if (cacheConfig == null)
            {
                cacheConfig = new CacheStorageConfig();
            }
            return cacheConfig;
        }

        internal static CachePackage LoadPackage()
        {
            cachePackage = CachePackage.Load();
            if (cachePackage == null)
            {
                cachePackage = new CachePackage();
            }

            return cachePackage;
        }
        internal static void SavePackage()
        {
            Package.Save();

            if (onChangeCache != null)
            {
                onChangeCache();
            }
        }

    }
}