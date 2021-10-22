using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Models;
using MyNotesApi.MicrosoftGraphClient;
using System.Text.Json;
using Subscription = DataAccess.Entities.Subscription;

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
        private readonly ApplicationDbContext _context;

        public PabblySubscriptionsController(GraphClient graphClient,
                                             ILogger<WeatherForecastController> logger,
                                             IOptions<AzureAdB2CSettings> options,
                                             ApplicationDbContext context)
        {
            _graphClient = graphClient;
            _logger = logger;
            _azureAdB2CSettings = options.Value;
            _context = context;
            _azureAdB2CSettings = options.Value;
        }

        [HttpPost("subscription-created")]
        public async Task<IActionResult> SubscriptionCreated([FromBody] JsonElement message)
        {
            var webhookMessageDto = WebHookSubscriptionCreatedDto.TryGetPropertyValues(message);

            var filter = $"identities/any(id:id/Issuer eq '{_azureAdB2CSettings.TenantId}' and id/IssuerAssignedId eq '{webhookMessageDto.Email}')";
            var userInB2cAndGraph = await _graphClient.ServiceClient.Users.Request().Filter(filter).GetAsync();
            var userId = userInB2cAndGraph.SingleOrDefault()?.Id;

            // call graph to add subscriptionId and trialExpiryDate as attributes
            if (userId is not null)
            {
                var user = new User()
                {
                    AdditionalData = ConstructCustomAttributeWithValues(_azureAdB2CSettings, webhookMessageDto.SubscriptionId, webhookMessageDto.TrialExpiryDate.ToUniversalTime())
                };

                await _graphClient.ServiceClient.Users[$"{userId}"].Request().UpdateAsync(user);
            }


            // save to database
            var entity = new Subscription()
            {
                SubscriptionId = webhookMessageDto.SubscriptionId,
                Currency = webhookMessageDto.Currency,
                Email = webhookMessageDto.Email,
                TrialExpired = webhookMessageDto.TrialExpiryDate,
                CustomerId = webhookMessageDto.CustomerId,
            };

            await _context.Subscriptions.AddAsync(entity);

            var response = await _context.SaveChangesAsync();

            if (response > 0)
                await _context.DisposeAsync();

            return Ok();
        }

        private IDictionary<string, object> ConstructCustomAttributeWithValues(AzureAdB2CSettings b2cConfig,
                                                                               string subscriptionId,
                                                                               DateTimeOffset trialExpired)
            => new Dictionary<string, object>()
            {
                { b2cConfig.SubscriptionAttribute.ToString(), subscriptionId },
                { b2cConfig.TrialExpiredAttribute.ToString(), trialExpired }
            };
    }
}