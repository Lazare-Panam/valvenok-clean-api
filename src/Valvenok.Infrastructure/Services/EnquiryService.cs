using Valvenok.Api.DTO;
using Valvenok.Core.Common.Errors;
using Valvenok.Core.Entities.User;
using Valvenok.Core.Interfaces;
using Valvenok.Core.Interfaces.Users;
using Valvenok.Infrastructure.Data;

namespace Valvenok.Infrastructure.Services
{
    public class EnquiryService : IEnquiryService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly INotificationService _notificationService;

        public EnquiryService(ApplicationDbContext dbContext, INotificationService notificationService)
        {
            _dbContext = dbContext;
            _notificationService = notificationService;
        }


        public async Task<Result> CreateEnquiry(CreateEnquiryRequest enquiryRequest, CancellationToken ct)
        {
            var enquiry = new Enquiry(
                enquiryRequest.Name,
                enquiryRequest.Subject,
                enquiryRequest.Message,
                enquiryRequest.Email,
                enquiryRequest.PhoneNo,
                enquiryRequest.AttachmentLink,
                DateTimeOffset.UtcNow,
                company: enquiryRequest.Company,
                expectedLeadTime: enquiryRequest.ExpectedLeadTime);
            /*TODO: Rishik handle duplicate entries*/
            _dbContext.Enquiries.Add(enquiry);
            await _dbContext.SaveChangesAsync(ct);
            await _notificationService.HandleNewEnquiryAsync(enquiry, ct);
            return Result.Success();
        }
    }
}
