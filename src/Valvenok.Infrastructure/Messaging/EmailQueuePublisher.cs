using System.Text.Json;
using Azure.Storage.Queues;
using Microsoft.Extensions.Logging;
using Valvenok.Core.Entities.Messaging;
using Valvenok.Core.Interfaces;

namespace Valvenok.Infrastructure.Messaging
{
    public class EmailQueuePublisher : IEmailQueuePublisher
    {
        private readonly QueueClient _queueClient;
        private readonly ILogger<EmailQueuePublisher> _logger;
        private const string queueName = "notifications";
        public EmailQueuePublisher(QueueServiceClient queueServiceClient, ILogger<EmailQueuePublisher> logger)
        {
            _queueClient = queueServiceClient.GetQueueClient(queueName);
            _logger = logger;
        }
        public async Task EnqueueAsync(EmailNotification message, CancellationToken ct = default)
        {
            await _queueClient.CreateIfNotExistsAsync(cancellationToken: ct);
            var payload = JsonSerializer.Serialize(new
            {
                email = message.Email,
                subject = message.Subject,
                body = message.HtmlBody
            });
            var receipt = await _queueClient.SendMessageAsync(payload, ct);
            _logger.LogInformation("Enqueued email notification {MessageId} subject {Subject}", receipt.Value.MessageId, message.Subject);
        }
    }
}
