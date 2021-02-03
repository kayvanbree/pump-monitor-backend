using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace pump_monitor_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TickersController : ControllerBase
    {
        private static readonly string[] Tickers = new[]
        {
            "MTHBTC"
        };

        private readonly ILogger<TickersController> _logger;

        public TickersController(ILogger<TickersController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        
        public IEnumerable<string> Get()
        {
            return Tickers;
        }
    }
}