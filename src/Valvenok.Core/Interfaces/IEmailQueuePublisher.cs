using Valvenok.Core.Entities.Messaging;

namespace Valvenok.Core.Interfaces
{
    public interface IEmailQueuePublisher
    {
        Task EnqueueAsync(EmailNotification message, CancellationToken ct = default);
    }
}
