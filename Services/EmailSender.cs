using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mandrill;
using Mandrill.Models;
using Mandrill.Requests.Messages;
using Microsoft.Extensions.Options;

namespace MyNotesApi.Services
{
    public class EmailSender : ISendEmail
    {
        private readonly MandrillApi _mandrillApi;
        private readonly EmailServiceConfig _emailServiceConfig;

        public EmailSender(IOptions<EmailServiceConfig> options)
        {
            _emailServiceConfig = options.Value;
            _mandrillApi = new MandrillApi(_emailServiceConfig.MandrillApiKey);
        }

        public async Task SendWelcomeEmailAsync(string username,
                                                string email)
        {
            var to = new List<EmailAddress>
            {
                new EmailAddress
                {
                    Email = email,
                    Name = username
                }
            };
            var message = ConstructMessage(_emailServiceConfig.EmailFromAddress,
                                           _emailServiceConfig.EmailFromName,
                                           _emailServiceConfig.WelcomeEmailSubject,
                                           to);

            var variables = new Dictionary<string, string>
            {
                { "username", username },
                { "subject", _emailServiceConfig.WelcomeEmailSubject },
                { "current_year", DateTime.UtcNow.Date.Year.ToString() }
            };

            EmailTemplateVariableReplacement(message, variables);

            var request = new SendMessageTemplateRequest(message, _emailServiceConfig.WelcomeEmailTemplateName);
            await _mandrillApi.SendMessageTemplate(request);
        }

        private void EmailTemplateVariableReplacement(EmailMessage message,
                                                      IDictionary<string, string> variables)
        {
            foreach (var (key, value) in variables)
                message.AddGlobalVariable(key, value);
        }

        private EmailMessage ConstructMessage(string fromEmail,
                                              string fromName,
                                              string subject,
                                              List<EmailAddress> to)
        {
            var constructedMessage = new EmailMessage
            {
                FromEmail = fromEmail,
                FromName = fromName,
                Subject = subject,
                To = to
            };

            return constructedMessage;
        }
    }
}