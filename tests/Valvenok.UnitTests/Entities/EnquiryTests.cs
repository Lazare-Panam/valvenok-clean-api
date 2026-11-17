using Valvenok.Core.Entities.Enums;
using Valvenok.Core.Entities.User;

namespace Valvenok.UnitTests.Entities;

public class EnquiryTests
{
    // Small factory so each test reads cleanly and only varies what it cares about.
    private static Enquiry CreateSut(
        string name = "John Doe", string subject = "Quote request", string message = "Please quote valves",
        string email = "john@acme.com", string phoneNo = "+441234567890", string? attachmentLink = null,
        DateTimeOffset? createdAt = null, string company = "Acme", string? expectedLeadTime = "2-3 weeks")
        => new(name, subject, message, email, phoneNo, attachmentLink,
               createdAt ?? DateTimeOffset.UtcNow, company, expectedLeadTime);

    [Fact]
    public void Constructor_NewEnquiry_StatusIsReceived()
    {
        var enquiry = CreateSut();
        Assert.Equal(EnquiryStatus.Received, enquiry.Status);
    }

    [Fact]
    public void Constructor_WhitespaceAroundTextFields_AreTrimmed()
    {
        var enquiry = CreateSut(name: "  John Doe  ", subject: "  Quote  ",
            message: "  hello  ", email: "  john@acme.com  ", phoneNo: "  +441234567890  ");

        Assert.Equal("John Doe", enquiry.Name);
        Assert.Equal("Quote", enquiry.Subject);
        Assert.Equal("hello", enquiry.Message);
        Assert.Equal("john@acme.com", enquiry.Email);
        Assert.Equal("+441234567890", enquiry.PhoneNo);
    }

    [Fact]
    public void Constructor_OptionalFieldsAndCreatedAt_ArePreserved()
    {
        var createdAt = new DateTimeOffset(2026, 1, 1, 12, 0, 0, TimeSpan.Zero);
        var enquiry = CreateSut(createdAt: createdAt, attachmentLink: "https://x/y.pdf", expectedLeadTime: "ASAP");

        Assert.Equal(createdAt, enquiry.CreatedAt);
        Assert.Equal("https://x/y.pdf", enquiry.AttachmentLink);
        Assert.Equal("ASAP", enquiry.ExpectedLeadTime);
    }

    [Fact]
    public void MarkNotified_Called_StatusIsNotified()
    {
        var enquiry = CreateSut();
        enquiry.MarkNotified();
        Assert.Equal(EnquiryStatus.Notified, enquiry.Status);
    }

    [Fact]
    public void MarkNotifiedFailed_Called_StatusIsNotifyFailed()
    {
        var enquiry = CreateSut();
        enquiry.MarkNotifiedFailed();
        Assert.Equal(EnquiryStatus.NotifyFailed, enquiry.Status);
    }
}
