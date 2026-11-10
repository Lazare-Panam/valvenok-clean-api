using System;
using System.Collections.Generic;
using System.Text;
using Valvenok.Core.Entities.Enums;
using Valvenok.Core.Interfaces;

namespace Valvenok.Core.Entities.User
{
    public class Enquiry : BaseEntity , IAggregateRoot
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string PhoneNo { get; private set; }
        public string Subject { get; private set; }
        public string Message { get; private set; }
        public string? AttachmentLink { get; private set; }
        public EnquiryStatus Status { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public Enquiry(string name, string subject, string message, string email, string phoneNo, string? attachmentLink, DateTimeOffset createdAt)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentException.ThrowIfNullOrWhiteSpace(email);
            ArgumentException.ThrowIfNullOrWhiteSpace(message);
            ArgumentException.ThrowIfNullOrWhiteSpace(subject);
            ArgumentException.ThrowIfNullOrWhiteSpace(phoneNo);
            Name = name.Trim();
            Message = message.Trim();
            Subject = subject.Trim();
            Email = email.Trim();
            PhoneNo = phoneNo.Trim();
            Status = EnquiryStatus.Received;
            AttachmentLink = attachmentLink;
            CreatedAt = createdAt;
        }
        public void MarkNotified() => Status = EnquiryStatus.Notified;
        public void MarkNotifiedFailed() => Status = EnquiryStatus.NotifyFailed;
    }
}
