using System.Collections.Generic;
using System.Threading.Tasks;
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

        [HttpGet("marketinfo")]
        [HttpOptions]
        public async Task<ActionResult<IEnumerable<string>>> GetMarketInfo() =>
            Ok(await _systemService.GetMarketInfo());
    }
}