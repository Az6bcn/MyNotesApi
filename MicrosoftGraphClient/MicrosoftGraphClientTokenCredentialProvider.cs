using System.Net.Http.Headers;
using Azure.Core;
using Azure.Identity;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Identity.Web;

// https://cmatskas.com/create-a-protected-api-that-calls-in-ms-graph-on-behalf-of-a-power-app/
//https://docs.microsoft.com/en-us/graph/sdks/choose-authentication-providers?view=graph-rest-1.0&tabs=CS#client-credentials-provider
namespace MyNotesApi.MicrosoftGraphClient
{
    public class MicrosoftGraphClientTokenCredentialProvider
    {
        private readonly AzureAdB2CSettings _azureAdB2CSettings;

        public MicrosoftGraphClientTokenCredentialProvider(IOptions<AzureAdB2CSettings> options)
        {
            _azureAdB2CSettings = options.Value;
        }
        
        public TokenCredential GetProvider()
        {
            var options = new TokenCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            };
            
            var provider = new ClientSecretCredential(_azureAdB2CSettings.TenantId,
                                                      _azureAdB2CSettings.ClientId,
                                                      _azureAdB2CSettings.Secret,
                                                      options);

            return provider;
        }
    }
}