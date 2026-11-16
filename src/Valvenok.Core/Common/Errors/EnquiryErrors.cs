namespace Valvenok.Core.Common.Errors
{
    public static class EnquiryErrors
    {
        public static readonly Error Duplicate = new(
            "Enquiry.Duplicate",
            "An identical enquiry was submitted moments ago. Please wait before trying again.");
    }
}
