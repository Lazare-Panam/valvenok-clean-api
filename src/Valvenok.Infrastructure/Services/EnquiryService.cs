using Microsoft.EntityFrameworkCore;
using Valvenok.Core.Entities.DTO;
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

            /*TODO: Rishik handle TOCOTU Bug (Concurrency) and look towards using one DB round trip instead of two*/
            var cutOffTime = DateTimeOffset.UtcNow.AddMinutes(-5);
            bool isDuplicate = await _dbContext.Enquiries.AnyAsync(e =>
            e.Email == enquiryRequest.Email &&
            e.Subject == enquiryRequest.Subject &&
            e.Message == enquiryRequest.Message &&
            e.CreatedAt >= cutOffTime, ct);

            if (isDuplicate)
                return Result.Failure(EnquiryErrors.Duplicate);

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

            _dbContext.Enquiries.Add(enquiry);
            await _dbContext.SaveChangesAsync(ct);
            await _notificationService.HandleNewEnquiryAsync(enquiry, ct);
            return Result.Success();
        }
    }
}
