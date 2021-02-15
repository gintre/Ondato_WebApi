using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Ondato_WebApi.Logic.Interfaces;
using Ondato_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ondato_WebApi.Logic
{
    public class DataLoadLogic : IDataLoadLogic
    {
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _config;
        public DataLoadLogic(IMemoryCache memoryCache, IConfiguration config)
        {
            _cache = memoryCache;
            _config = config;
        }

        public async Task CreateUpdate(string key, CherryItem cherry)
        {
            if(cherry.ExpirationPeriod.HasValue)
            {
                _cache.Set(key, cherry, (DateTimeOffset)cherry.ExpirationPeriod);
            }
            else
            {
                _cache.Set(key, cherry, GetCherriesDefaultCleanupTime());
            }
        }

        public async Task Delete(string key)
        {
            CherryItem existingItem;
            if (!_cache.TryGetValue(key, out existingItem))
            {
                throw new KeyNotFoundException();
            }

            _cache.Remove(key);
        }

        public async Task<CherryItem> Get(string key)
        {
            CherryItem existingItem;
            if (!_cache.TryGetValue(key, out existingItem))
            {
                throw new KeyNotFoundException();
            }

            return existingItem;
        }

        private DateTimeOffset GetCherriesDefaultCleanupTime()
        {
            var timeFromCache = _config.GetValue<int>("BusinessConstants:CleanupIntervalInMinutes");

            return DateTimeOffset.Now.AddMinutes(timeFromCache);
        }
    }
}
