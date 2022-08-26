using UnityEngine;
using System;
using System.IO;

namespace Gpm.CacheStorage
{
    [Serializable]
    public class CacheStorageConfig
    {
        public const string CONFIG_NAME = "CacheStorageConfig";

        [SerializeField]
        private string cachePath;

        [SerializeField]
        private int maxCount;

        [SerializeField]
        private long maxSize;

        [SerializeField]
        private long reRequestTime;
        
        public string GetCachePath()
        {
            if (string.IsNullOrEmpty(cachePath) == true)
            {
                SetCachePath(Application.temporaryCachePath);
            }

            return cachePath;
        }

        public void SetCachePath(string path)
        {
            cachePath = Path.Combine(path, GpmCacheStorage.NAME);
            Save();
        }

        public int GetMaxCount()
        {
            return maxCount;
        }

        public void SetMaxCount(int value)
        {
            maxCount = value;
            Save();
        }

        public long GetMaxSize()
        {
            return maxSize;
        }

        public void SetMaxSize(long value)
        {
            maxSize = value;
            Save();
        }
        public void SetReRequestTime(long value)
        {
            reRequestTime = value;
            Save();
        }

        public long GetReRequestTime()
        {
            return reRequestTime;
        }

        private static string ConfigPath()
        {
            return Path.Combine(Application.persistentDataPath, CONFIG_NAME);
        }

        public static CacheStorageConfig Load()
        {
            CacheStorageConfig config = null;
            try
            {
                config = JsonUtility.FromJson<CacheStorageConfig>(File.ReadAllText(ConfigPath()));
            }
            catch (Exception e)
            {
                Debug.LogWarning(e.Message);
            }

            return config;
        }

        public void Save()
        {
            try
            {
                File.WriteAllText(ConfigPath(), JsonUtility.ToJson(this));
            }
            catch(Exception ex)
            {
                Debug.LogException(ex);
            }
        }
        
    }
} 