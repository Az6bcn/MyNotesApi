
//https://cmatskas.com/create-a-protected-api-that-calls-in-ms-graph-on-behalf-of-a-power-app/

using Microsoft.Graph;

namespace MyNotesApi.MicrosoftGraphClient
{
    public class GraphClient
    {
        private GraphServiceClient _graphServiceClient;

        public GraphServiceClient ServiceClient
        {
            get
            {
                if (_graphServiceClient is not null)
                    return _graphServiceClient;
                
                _graphServiceClient = new GraphServiceClient(AuthenticationProvider.GetProvider());
                return _graphServiceClient;
            }
        }

        public MicrosoftGraphClientAuthProvider AuthenticationProvider { get; }

        public GraphClient(MicrosoftGraphClientAuthProvider authProvider)
        {
            AuthenticationProvider = authProvider;
        }
    }
}