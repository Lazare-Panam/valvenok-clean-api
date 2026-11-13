using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Valvenok.Core.Entities.Messaging;
using Valvenok.Core.Entities.User;
using Valvenok.Core.Interfaces;
using Valvenok.Infrastructure.Data.Config.Settings;

namespace Valvenok.Infrastructure.Notification
{
    public class NotificationService : INotificationService
    {
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IEmailQueuePublisher _emailQueue;
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(IEmailTemplateService templateService, IEmailQueuePublisher emailQueue, IOptions<EmailSettings> options, ILogger<NotificationService> logger)
        {
            _emailTemplateService = templateService;
            _emailQueue = emailQueue;
            _emailSettings = options.Value;
            _logger = logger;
        }
        public async Task HandleNewEnquiryAsync(Enquiry enquiry, CancellationToken ct = default)
        {
            await SendEnquiryInternalNotificationAsync(enquiry, ct);
            await SendEnquiryReceiptAsync(enquiry, ct);
        }
        private async Task SendEnquiryReceiptAsync(Enquiry enquiry, CancellationToken ct)
        {
            var body = await _emailTemplateService.GetEnquiryReceiptHtmlAsync(
                name: enquiry.Name, subject: enquiry.Subject, message: enquiry.Message);
            await _emailQueue.EnqueueAsync(new EmailNotification(enquiry.Email, "We've received your enquiry", body), ct);
        }
        private async Task SendEnquiryInternalNotificationAsync(Enquiry enquiry, CancellationToken ct)
        {
            var body = await _emailTemplateService.GetEnquiryInternalHtmlAsync(
                name: enquiry.Name, email: enquiry.Email, subject: enquiry.Subject, message: enquiry.Message);
            var subject = string.IsNullOrWhiteSpace(enquiry.Company)
                ? $"New Enquiry from {enquiry.Name}"
                : $"New Enquiry from {enquiry.Company}";
            await _emailQueue.EnqueueAsync(new EmailNotification(_emailSettings.InternalAddressEmail, subject, body), ct);
        }

    }
}
