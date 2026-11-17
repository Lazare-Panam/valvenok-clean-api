using Valvenok.Core.Entities.User;
namespace Valvenok.UnitTests.Builders;
public class EnquiryBuilder
{
    private string _name = "John Doe";
    private string _subject = "Quote request";
    private string _message = "Please quote valves";
    private string _email = "john@acme.com";
    private string _phoneNo = "+441234567890";
    private string? _attachmentLink;
    private DateTimeOffset _createdAt = new(2026, 1, 1, 12, 0, 0, TimeSpan.Zero);
    private string _company = "Acme";
    private string? _expectedLeadTime = "2-3 weeks";
    public EnquiryBuilder WithName(string name) { _name = name; return this; }
    public EnquiryBuilder WithSubject(string subject) { _subject = subject; return this; }
    public EnquiryBuilder WithMessage(string message) { _message = message; return this; }
    public EnquiryBuilder WithEmail(string email) { _email = email; return this; }
    public EnquiryBuilder WithPhoneNo(string phoneNo) { _phoneNo = phoneNo; return this; }
    public EnquiryBuilder WithAttachmentLink(string? attachmentLink) { _attachmentLink = attachmentLink; return this; }
    public EnquiryBuilder WithCreatedAt(DateTimeOffset createdAt) { _createdAt = createdAt; return this; }
    public EnquiryBuilder WithCompany(string company) { _company = company; return this; }
    public EnquiryBuilder WithExpectedLeadTime(string? expectedLeadTime) { _expectedLeadTime = expectedLeadTime; return this; }

    public Enquiry Build() =>
        new(_name, _subject, _message, _email, _phoneNo, _attachmentLink, _createdAt, _company, _expectedLeadTime);
}
