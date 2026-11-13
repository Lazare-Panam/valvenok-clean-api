using Valvenok.Api.DTO;
using Valvenok.Core.Common.Errors;

namespace Valvenok.Core.Interfaces.Users
{
    public interface IEnquiryService
    {
        // TODO: Rishik split wire DTO from service input
        Task<Result> CreateEnquiry(CreateEnquiryRequest enquiryRequest, CancellationToken ct);
    }
}
