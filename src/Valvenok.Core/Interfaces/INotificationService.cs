using Valvenok.Core.Entities.User;

namespace Valvenok.Core.Interfaces
{
    public interface INotificationService
    {
        Task HandleNewEnquiryAsync(Enquiry enquiry, CancellationToken ct = default);
    }
}
