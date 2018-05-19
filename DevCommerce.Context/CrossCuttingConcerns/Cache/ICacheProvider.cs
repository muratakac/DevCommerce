using System;

namespace DevCommerce.Core.CrossCuttingConcerns.Cache
{
    public interface ICacheProvider
    {
        void Set(string key, string value);

        void Set(string key, string value, TimeSpan timeout);

        string Get(string key);

        bool Remove(string key);

        bool IsInCache(string key);
    }
}
