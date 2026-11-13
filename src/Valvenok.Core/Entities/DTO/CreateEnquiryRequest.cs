namespace Valvenok.Api.DTO
{
    public record CreateEnquiryRequest
    {
        public string Email { get; set; } = string.Empty;
        public string PhoneNo { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? AttachmentLink { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string ExpectedLeadTime { get; set; } = string.Empty;
    }
}
