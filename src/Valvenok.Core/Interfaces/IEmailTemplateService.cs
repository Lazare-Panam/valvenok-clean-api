namespace Valvenok.Core.Interfaces
{
    public interface IEmailTemplateService
    {
        // To the team — full enquiry details.
        Task<string> GetEnquiryInternalHtmlAsync(string name, string email, string subject, string message);

        // To the customer — confirmation that we received their enquiry.
        Task<string> GetEnquiryReceiptHtmlAsync(string name, string subject, string message);
    }
}
