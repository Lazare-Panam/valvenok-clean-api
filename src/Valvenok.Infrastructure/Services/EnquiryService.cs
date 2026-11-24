using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<EnquiryService> _logger;
        public EnquiryService(ApplicationDbContext dbContext, INotificationService notificationService, ILogger<EnquiryService> logger)
        {
            _dbContext = dbContext;
            _notificationService = notificationService;
            _logger = logger;
        }
        public async Task<Result> CreateEnquiry(CreateEnquiryRequest enquiryRequest, CancellationToken ct)
        {

            /*TODO: Rishik handle TOCTOU Bug (Concurrency) and look towards using one DB round trip instead of two*/
            var cutOffTime = DateTimeOffset.UtcNow.AddMinutes(-5);
            bool isDuplicate = await _dbContext.Enquiries.AnyAsync(e =>
            e.Email == enquiryRequest.Email &&
            e.Subject == enquiryRequest.Subject &&
            e.Message == enquiryRequest.Message &&
            e.CreatedAt >= cutOffTime, ct);

            if (isDuplicate)
            {
                _logger.LogWarning("Duplicate enquiry skipped (subject {Subject})", enquiryRequest.Subject);
                return EnquiryErrors.Duplicate(enquiryRequest.Message);
            }

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
            _logger.LogInformation("Enquiry created for {Company} (subject {Subject})", enquiryRequest.Company, enquiryRequest.Subject);
            await _notificationService.HandleNewEnquiryAsync(enquiry, ct);
            return Result.Success();
        }
    }
}
