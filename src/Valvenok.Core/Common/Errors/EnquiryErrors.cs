namespace Valvenok.Core.Common.Errors
{
    public static class EnquiryErrors
    {
        public static Error Duplicate(string message) =>
        new("Enquiry.Exists", $"enquiry with message {message} already exists", ErrorType.Conflict);
    }
}
