using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Binance.Net;
using pump_monitor_backend.Models;

namespace pump_monitor_backend.Services
{
    public class SystemService : ISystemService
    {
        protected readonly IMemoryCache cache;
        protected readonly MemoryCacheEntryOptions cacheOptions;
        private const string MarketInfoCacheKey = "MARKET_INFO";
        
        public SystemService(IMemoryCache cache, MemoryCacheEntryOptions cacheOptions)
        {
            this.cache = cache;
            this.cacheOptions = cacheOptions;
        }

        public async Task<Object> GetMarketInfo()
        {
            if (!cache.TryGetValue(MarketInfoCacheKey, out Object marketInfo))
            {
                using (var client = new BinanceClient())
                {
                    //marketInfo = client.Spot.System.GetExchangeInfo();
                    marketInfo = new MarketInfo()
                    {
                        Data = new[] {"MTHBTC"}
                    };
                    cache.Set(MarketInfoCacheKey, marketInfo, cacheOptions);
                }
            }

            return marketInfo;
        }
    }
}