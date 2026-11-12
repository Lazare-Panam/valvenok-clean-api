using Valvenok.Api.DTO;

namespace Valvenok.Core.Interfaces.Users
{
    public interface IEnquiryService
    {
        // TODO: Rishik split wire DTO from service input
        Task CreateEnquiry(CreateEnquiryRequest enquiryRequest, CancellationToken ct);
    }
}
