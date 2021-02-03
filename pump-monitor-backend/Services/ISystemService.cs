using System;
using System.Threading.Tasks;

namespace pump_monitor_backend.Services
{
    public interface ISystemService
    {
        Task<Object> GetMarketInfo();
    }
}