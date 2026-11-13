using FluentValidation;
using Valvenok.Api.DTO;

namespace Valvenok.Core.Entities.Validations
{
    public class CreateEnquiryRequestValidator : AbstractValidator<CreateEnquiryRequest>
    {
        public CreateEnquiryRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(254);
            RuleFor(x => x.Subject).NotEmpty().MaximumLength(150);
            RuleFor(x => x.Message).NotEmpty().MaximumLength(500);
            RuleFor(x => x.PhoneNo).NotEmpty().MaximumLength(20);
        }
    }
}

