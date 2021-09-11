using System.Net.Http.Headers;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Identity.Web;

// https://cmatskas.com/create-a-protected-api-that-calls-in-ms-graph-on-behalf-of-a-power-app/
namespace MyNotesApi.MicrosoftGraphClient
{
    public class MicrosoftGraphClientAuthProvider
    {
        private DelegateAuthenticationProvider _authProvider;
        private readonly GraphApiSettings _graphApiSettings;

        public MicrosoftGraphClientAuthProvider(ITokenAcquisition token,
                                                IOptions<GraphApiSettings> options)
        {
            Token = token;
            _graphApiSettings = options.Value;
        }

        private ITokenAcquisition Token { get; }

        public DelegateAuthenticationProvider GetProvider()
        {
            if (_authProvider is not null)
                return _authProvider;


            var provider = new DelegateAuthenticationProvider( x =>
            {
                var accessToken
                    =  Token.GetAccessTokenForAppAsync(_graphApiSettings.Scope).Result;

                x.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                return null;
            });

            return provider;
        }
    }
}