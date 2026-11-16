using System.Net;
using System.Reflection;
using Valvenok.Core.Interfaces;

namespace Valvenok.Infrastructure.Notification
{
    public class EmailTemplateService : IEmailTemplateService
    {
        // To the team — full enquiry details.
        public Task<string> GetEnquiryInternalHtmlAsync(string name, string email, string subject, string message)
        {
            var html = LoadTemplate("EnquiryRecieved.html");
            var replacements = new Dictionary<string, string>
            {
                { "{{CustomerName}}", WebUtility.HtmlEncode(name) },
                { "{{CustomerEmail}}", WebUtility.HtmlEncode(email) },
                { "{{InquirySubject}}", WebUtility.HtmlEncode(subject) },
                { "{{CustomerMessage}}", WebUtility.HtmlEncode(message) }
            };
            return Task.FromResult(ReplaceTokens(html, replacements));
        }

        // To the customer — confirmation that we received their enquiry.
        public Task<string> GetEnquiryReceiptHtmlAsync(string name, string subject, string message)
        {
            var html = LoadTemplate("EnquiryConfirmation.html");
            var replacements = new Dictionary<string, string>
            {
                { "{{CustomerName}}", WebUtility.HtmlEncode(name) },
                { "{{InquirySubject}}", WebUtility.HtmlEncode(subject) },
                { "{{CustomerMessage}}", WebUtility.HtmlEncode(message) }
            };
            return Task.FromResult(ReplaceTokens(html, replacements));
        }

        private static string LoadTemplate(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourcePath = $"{assembly.GetName().Name}.EmailTemplates.{fileName}";

            using var stream = assembly.GetManifestResourceStream(resourcePath)
                ?? throw new FileNotFoundException($"Email template not found: {resourcePath}");

            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        private static string ReplaceTokens(string template, Dictionary<string, string> replacements)
        {
            foreach (var replacement in replacements)
            {
                template = template.Replace(replacement.Key, replacement.Value ?? string.Empty);
            }

            return template;
        }
    }
}
