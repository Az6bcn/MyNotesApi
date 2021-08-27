using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Identity.Web;

namespace MyNotesApi.GraphClient
{
    public class AuthProvider
    {
        private DelegateAuthenticationProvider _authProvider;
        private readonly GraphApiSettings _graphApiSettings;
        
        public AuthProvider(ITokenAcquisition token, IOptions<GraphApiSettings> options)
        {
            Token = token;
            _graphApiSettings = options.Value;
        }

        private ITokenAcquisition Token { get; }
        public DelegateAuthenticationProvider GetProvider()
        {
            if (_authProvider is not null)
                return _authProvider;


            var provider = new DelegateAuthenticationProvider(async x =>
            {
                var accessToken
                    = await Token.GetAccessTokenForAppAsync(_graphApiSettings.Scope, _graphApiSettings.TenantId);
            });

            return provider;
        }
    }
}