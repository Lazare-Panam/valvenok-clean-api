
using System.Xml.Linq;

namespace Valvenok.Core.Entities.Messaging
{
    public record EmailNotification
    {
        public string Email { get; }
        public string Subject { get; }
        public string HtmlBody { get; }
        public EmailNotification(string email, string subject, string htmlBody)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(email);
            ArgumentException.ThrowIfNullOrWhiteSpace(subject);
            ArgumentException.ThrowIfNullOrWhiteSpace(htmlBody);
            Email = email.Trim();
            Subject = subject.Trim();
            HtmlBody = subject.Trim();
        }
    }
}
