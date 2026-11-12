using Valvenok.Api.DTO;
using Valvenok.Core.Entities.User;
using Valvenok.Core.Interfaces.Users;
using Valvenok.Infrastructure.Data;
using Valvenok.Infrastructure.Notification;

namespace Valvenok.Infrastructure
{
    public class EnquiryService : IEnquiryService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly NotificationService _notificationService;

        public EnquiryService(ApplicationDbContext dbContext, NotificationService notificationService)
        {
            _dbContext = dbContext;
            _notificationService = notificationService;
        }

        public async Task CreateEnquiry(CreateEnquiryRequest enquiryRequest, CancellationToken ct)
        {
            var enquiry = new Enquiry(
                enquiryRequest.Name,
                enquiryRequest.Subject,
                enquiryRequest.Message,
                enquiryRequest.Email,
                enquiryRequest.PhoneNo,
                enquiryRequest.AttachmentLink,
                DateTimeOffset.UtcNow,
                enquiryRequest.Company);

            _dbContext.Enquiries.Add(enquiry);
            await _dbContext.SaveChangesAsync(ct);
            await _notificationService.HandleNewEnquiryAsync(enquiry, ct);
        }
    }
}
