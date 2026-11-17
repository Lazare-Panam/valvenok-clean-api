using Valvenok.Core.Entities.Enums;
using Valvenok.UnitTests.Builders;

namespace Valvenok.UnitTests.Core.Entities.User;

public class EnquiryTests
{
    [Fact]
    public void Constructor_NewEnquiry_StatusIsReceived()
    {
        var enquiry = new EnquiryBuilder().Build();

        Assert.Equal(EnquiryStatus.Received, enquiry.Status);
    }

    [Fact]
    public void Constructor_WhitespaceAroundTextFields_AreTrimmed()
    {
        var enquiry = new EnquiryBuilder()
            .WithName("  John Doe  ")
            .WithSubject("  Quote  ")
            .WithMessage("  hello  ")
            .WithEmail("  john@acme.com  ")
            .WithPhoneNo("  +441234567890  ")
            .Build();

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

        var enquiry = new EnquiryBuilder()
            .WithCreatedAt(createdAt)
            .WithAttachmentLink("https://x/y.pdf")
            .WithExpectedLeadTime("ASAP")
            .Build();

        Assert.Equal(createdAt, enquiry.CreatedAt);
        Assert.Equal("https://x/y.pdf", enquiry.AttachmentLink);
        Assert.Equal("ASAP", enquiry.ExpectedLeadTime);
    }

    [Fact]
    public void MarkNotified_Called_StatusIsNotified()
    {
        var enquiry = new EnquiryBuilder().Build();

        enquiry.MarkNotified();

        Assert.Equal(EnquiryStatus.Notified, enquiry.Status);
    }

    [Fact]
    public void MarkNotifiedFailed_Called_StatusIsNotifyFailed()
    {
        var enquiry = new EnquiryBuilder().Build();

        enquiry.MarkNotifiedFailed();

        Assert.Equal(EnquiryStatus.NotifyFailed, enquiry.Status);
    }
}
