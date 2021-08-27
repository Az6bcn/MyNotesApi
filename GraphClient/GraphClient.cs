
//https://cmatskas.com/create-a-protected-api-that-calls-in-ms-graph-on-behalf-of-a-power-app/

using Microsoft.Graph;

namespace MyNotesApi.GraphClient
{
    public class GraphClient
    {
        private readonly GraphServiceClient _graphServiceClient;
        private MicrosoftGraphClientAuthProvider _authProvider;

        public GraphClient(MicrosoftGraphClientAuthProvider authProvider)
        {
            _authProvider = authProvider;
            _graphServiceClient = new GraphServiceClient(_authProvider.GetProvider());
        }
    }
}