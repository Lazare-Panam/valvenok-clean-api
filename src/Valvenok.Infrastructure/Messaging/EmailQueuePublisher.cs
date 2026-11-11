using Azure.Storage.Queues;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;
using Valvenok.Core.Entities.Messaging;
using Valvenok.Core.Interfaces;
using Valvenok.Infrastructure.Data.Config.Settings;

namespace Valvenok.Infrastructure.Messaging
{
    public class EmailQueuePublisher : INotificationPublisher
    {
        private readonly QueueClient _queueClient;
        private readonly ILogger<EmailQueuePublisher> _logger;
        public EmailQueuePublisher(QueueClient queueClient, ILogger<EmailQueuePublisher> logger)
        {
            _queueClient = queueClient;
            _logger = logger;
        }
        public async Task EnqueueAsync(EmailNotification message, CancellationToken ct = default)
        {
            await _queueClient.CreateIfNotExistsAsync(cancellationToken: ct);
            var payload = JsonSerializer.Serialize(new { message });
            var receipt = await _queueClient.SendMessageAsync(payload, ct);
            _logger.LogInformation("Enqueued email notification {MessageId} subject {Subject}", receipt.Value.MessageId, message.Subject);
        }
    }
}
