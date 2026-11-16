using FluentValidation;
using Valvenok.Core.Entities.DTO;

namespace Valvenok.Api.Validations
{
    public class CreateEnquiryRequestValidator : AbstractValidator<CreateEnquiryRequest>
    {
        public CreateEnquiryRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(254);
            RuleFor(x => x.Company).MaximumLength(100);
            RuleFor(x => x.Subject).NotEmpty().MaximumLength(150);
            RuleFor(x => x.Message).NotEmpty().MaximumLength(500);
            RuleFor(x => x.PhoneNo).NotEmpty().MaximumLength(20);
            RuleFor(x => x.ExpectedLeadTime).MaximumLength(200);
        }
    }
}

