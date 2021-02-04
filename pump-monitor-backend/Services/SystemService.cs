using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Binance.Net;
using Binance.Net.Objects.Spot.MarketData;
using CryptoExchange.Net.Objects;
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

        public async Task<BinanceExchangeInfo> GetMarketInfo()
        {
            if (!cache.TryGetValue(MarketInfoCacheKey, out BinanceExchangeInfo marketInfo))
            {
                using (var client = new BinanceClient())
                {
                    WebCallResult<BinanceExchangeInfo> result = await client.Spot.System.GetExchangeInfoAsync();
                    // Check for errors here
                    marketInfo = result.Data;
                    cache.Set(MarketInfoCacheKey, marketInfo, cacheOptions);
                }
            }

            return marketInfo;
        }
    }
}