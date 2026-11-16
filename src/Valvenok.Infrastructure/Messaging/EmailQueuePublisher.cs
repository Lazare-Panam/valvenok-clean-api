using System.Text.Json;
using Azure.Storage.Queues;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Valvenok.Core.Entities.Messaging;
using Valvenok.Core.Interfaces;
using Valvenok.Infrastructure.Data.Config.Settings;

namespace Valvenok.Infrastructure.Messaging
{
    public class EmailQueuePublisher : IEmailQueuePublisher
    {
        private readonly QueueClient _queueClient;
        private readonly ILogger<EmailQueuePublisher> _logger;
        public EmailQueuePublisher(QueueServiceClient queueServiceClient, ILogger<EmailQueuePublisher> logger, IOptions<QueueSettings> options)
        {
            _queueClient = queueServiceClient.GetQueueClient(options.Value.QueueName);
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
