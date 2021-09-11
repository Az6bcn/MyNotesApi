﻿using System;
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
        public async Task<IActionResult> SubscriptionCreated([FromBody] JsonElement value)
        {
            // var eventType = value.event_type;
            // var plan = value.data.plan.plan_name;
            // var planCode = value.data.plan.plan_code;
            // var customerEmail = value.data.email_id;
            // var customerId = value.data.customer_id;
            // var trialExpiryDay = value.data.trial_expiry_date;
            
            // call microsoft graph
            // if (eventType == "subscription_create")
            // {
            //     //var user = 
            // }

            IEnumerable<User> users;
            try
            {
                var xx =  _graphClient.ServiceClient.Users;
                users = await _graphClient.ServiceClient.Users.Request().GetAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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