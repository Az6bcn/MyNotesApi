namespace MyNotesApi.Services
{
    public class EmailServiceConfig
    {
        public string MandrillApiKey { get; set; }
        public string EmailFromAddress { get; set; }
        public string EmailFromName { get; set; }
        public string WelcomeEmailSubject { get; set; }
        public string WelcomeEmailTemplateName { get; set; }
    }
}