using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Valvenok.Api.DTO;
using Valvenok.Core.Entities.Validations;
using Valvenok.Core.Interfaces.Users;

namespace Valvenok.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IEnquiryService _enquiryService;
        private readonly ILogger<UserController> _logger;

        public UserController(IEnquiryService enquiryService, ILogger<UserController> logger)
        {
            _enquiryService = enquiryService;
            _logger = logger;
        }

        [HttpPost("enquiry")]
        public async Task<IActionResult> SendEnquiry([FromBody] CreateEnquiryRequest enquiryRequest, CancellationToken ct)
        {
            var validator = new CreateEnquiryRequestValidator();
            ValidationResult validationResults = await validator.ValidateAsync(enquiryRequest, ct);
            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.ToDictionary());
            }
            var serviceResult = await _enquiryService.CreateEnquiry(enquiryRequest, ct);
            if(serviceResult.IsFailure)
            {
                return Conflict(new { code = serviceResult.Error.Code, message = serviceResult.Error.Description });
            }
            return Ok(new { message = "Enquiry sent successfully." });
        }
    }
}
