﻿using System.Threading.Tasks;
using Binance.Net.Objects.Spot.MarketData;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pump_monitor_backend.Services;

namespace pump_monitor_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SystemController : ControllerBase
    {
        private readonly ISystemService _systemService;

        private readonly ILogger<SystemController> _logger;

        public SystemController(ILogger<SystemController> logger, ISystemService systemService)
        {
            _logger = logger;
            this._systemService = systemService;
        }

        [EnableCors("AllowAll")]
        [HttpGet("marketinfo")]
        public async Task<ActionResult<BinanceExchangeInfo>> GetMarketInfo() =>
            Ok(await _systemService.GetMarketInfo());
    }
}