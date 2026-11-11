using System;
using System.Collections.Generic;
using System.Text;
using Valvenok.Api.DTO;

namespace Valvenok.Core.Interfaces.Users
{
    public interface IEnquiryService
    {
        // TODO: Rishik split wire DTO from service input
        public Task CreateEnquiry(CreateEnquiryRequest enquiryRequest, CancellationToken ct);
    }
}
