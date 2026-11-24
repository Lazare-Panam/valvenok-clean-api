namespace Valvenok.Core.Common.Errors
{
    public static class CatalogErrors
    {
        public static Error NotFound(string path) => new("Not.Found", $"Product with the path {path} does not exist", ErrorType.NotFound);
    }
}
