using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web.Resource;

namespace MyNotesApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MyNotesWebhooksController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public MyNotesWebhooksController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpPost("subscription-created")]
        public async Task<IActionResult> SubscriptionCreated([FromBody] dynamic eventData)
        {
            
            return Ok();
        }
    }
}