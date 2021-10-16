using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Identity.Web.Resource;
using MyNotesApi.MicrosoftGraphClient;
using MyNotesApi.Models;

namespace MyNotesApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("my-notes/api")]
    public class PabblySubscriptionsController : ControllerBase
    {
        private GraphClient _graphClient;
        private readonly ILogger<WeatherForecastController> _logger;

        public PabblySubscriptionsController(GraphClient graphClient,
                                             ILogger<WeatherForecastController> logger)
        {
            _graphClient = graphClient;
            _logger = logger;
        }

        [HttpPost("subscription-created")]
        public async Task<IActionResult> SubscriptionCreated([FromBody] JsonElement message)
        {
            var dto = new WebHookSubscriptionCreatedDto();
            dto.TryGetPropertyValues(message);

            var users = await _graphClient.ServiceClient.Users.Request().GetAsync();
            
            //users.Where(x => x.FI)
            
            return Ok();
        }
    }
}