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
    [Route("my-notes/api")]
    public class PabblySubscriptionsController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public PabblySubscriptionsController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpPost("subscription-created")]
        public async Task<IActionResult> SubscriptionCreated([FromBody] dynamic value)
        {
            var eventType = value.event_type;
            var plan = value.data.plan.plan_name;
            var planCode = value.data.plan.plan_code;
            var customerEmail = value.data.email_id;
            var customerId = value.data.customer_id;
            var trialExpiryDay = value.data.trial_expiry_date;
            
            // call microsoft graph
            if (eventType == "subscription_create")
            {
                //var user = 
            }
            return Ok();
        }
    }

    public  class WebHookDto
    {
        public string event_type { get; set; }
        public object plan { get; set; }
    }
}