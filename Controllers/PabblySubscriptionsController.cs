using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyNotesApi.MicrosoftGraphClient;
using MyNotesApi.Models;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace MyNotesApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("my-notes/api")]
    public class PabblySubscriptionsController : ControllerBase
    {
        private GraphClient _graphClient;
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly AzureAdB2CSettings _azureAdB2CSettings;

        public PabblySubscriptionsController(GraphClient graphClient,
                                             ILogger<WeatherForecastController> logger,
                                             IOptions<AzureAdB2CSettings> options)
        {
            _graphClient = graphClient;
            _logger = logger;
            _azureAdB2CSettings = options.Value;
        }

        [HttpPost("subscription-created")]
        public async Task<IActionResult> SubscriptionCreated([FromBody] JsonElement message)
        {
            var webhookMessageDto = new WebHookSubscriptionCreatedDto();
            webhookMessageDto.TryGetPropertyValues(message);

            var filter = $"identities/any(id:id/Issuer eq '{_azureAdB2CSettings.TenantId}' and id/IssuerAssignedId eq '{webhookMessageDto.Email}')";
            var user = await _graphClient.ServiceClient.Users.Request().Filter(filter).GetAsync();

            // call graph to add subscriptionId and trialExpiryDate as attributes

            // save to database


            return Ok();
        }

    }
}