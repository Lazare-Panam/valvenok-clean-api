using System;
using System.Collections.Generic;
using System.Text;
using Valvenok.Api.DTO;
using Valvenok.Core.Interfaces.Users;

namespace Valvenok.Infrastructure
{
    public class EnquiryService : IEnquiryService
    {
        public Task CreateEnquiry(CreateEnquiryRequest enquiryRequest, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
