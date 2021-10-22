namespace MyNotesApi.MicrosoftGraphClient
{
    public class AzureAdB2CSettings
    {
        public string TenantId { get; set; }
        public string AppId { get; set; }
        public string Secret { get; set; }
        public string Scope { get; set; }
        public string ClientId { get; set; }
        public string SubscriptionAttribute { get; set; }
        public string TrialExpiredAttribute { get; set; }
    }
}