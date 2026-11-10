using System;
using System.Collections.Generic;
using System.Text;
using Valvenok.Core.Entities.Messaging;

namespace Valvenok.Core.Interfaces
{
    public interface INotificationPublisher
    {
        Task EnqueueAsync(EmailNotification message, CancellationToken ct = default);
    }
}
