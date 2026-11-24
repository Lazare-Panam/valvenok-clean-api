namespace Valvenok.Core.Common.Errors
{
    public enum ErrorType
    {
        NotFound,
        Conflict
    }
    public sealed record Error(string Code, string Description, ErrorType Type);
}
