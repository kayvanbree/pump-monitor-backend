using System;
using System.Threading.Tasks;
using Binance.Net.Objects.Spot.MarketData;

namespace pump_monitor_backend.Services
{
    public interface ISystemService
    {
        Task<BinanceExchangeInfo> GetMarketInfo();
    }
}