using UnityEngine;
using System;


namespace Gpm.CacheStorage
{
    [Serializable]
    public class CacheInfo : IComparable<CacheInfo>
    {
        [Serializable]
        public struct CacheControl
        {
            public long maxAge;

            public bool mustRevalidate;
        }
        
        [NonSerialized]
        internal CachePackage storage;

        [SerializeField]
        public string url;

        [SerializeField]
        public string eTag;

        [SerializeField]
        public string lastModified;

        [SerializeField]
        public long lastAccess;

        [SerializeField]
        public string expires;

        [SerializeField]
        public long received;

        [SerializeField]
        public string age;

        [SerializeField]
        public string date;

        [SerializeField]
        public CacheControl cacheControl;

        [SerializeField]
        public long contentLength;

        [SerializeField]
        internal int index;

        public CacheInfo(CachePackage storage, string url)
        {
            this.storage = storage;
            this.url = url;
        }

        public CacheInfo(int index)
        {
            this.index = index;
        }

        public int CompareTo(CacheInfo other)
        {
            if (IsExpired() != other.IsExpired())
            {
                if (IsExpired() == true)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }

            long now = DateTime.UtcNow.Ticks;

            long accessPeriod = lastAccess - now;

            bool lastAccessMonth = IsLastAccessMonth();
            if (lastAccessMonth != other.IsLastAccessMonth())
            {
                if(lastAccessMonth == false)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }

            }

            bool lastAccessWeek = IsLastAccessWeek();
            if (lastAccessWeek != other.IsLastAccessWeek())
            {
                if (lastAccessWeek == false)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }

            }
            return -other.index.CompareTo(index);
        }

        private DateTime expireTime = default(DateTime);
        public DateTime GetExpireTime()
        {
            if (expireTime == default(DateTime))
            {
                if (string.IsNullOrEmpty(expires) == false)
                {
                    if (DateTime.TryParse(expires, out expireTime) == true)
                    {
                        return expireTime;
                    }
                }
            }
            
            return expireTime;
        }
        

        public bool IsExpired()
        {
            return GetExpireTime() < DateTime.UtcNow;
        }

        public bool IsLastAccessWeek()
        {
            long now = DateTime.UtcNow.Ticks;

            long accessPeriod = now - lastAccess;
            return accessPeriod < TimeSpan.TicksPerDay * 7;
        }

        public bool IsLastAccessMonth()
        {
            long now = DateTime.UtcNow.Ticks;

            long accessPeriod = now - lastAccess;
            return accessPeriod < TimeSpan.TicksPerDay * 30;
        }
    }
}