namespace Valvenok.Core.Interfaces
{
    public interface IFileStorage
    {
        Task<string> UploadFile(Stream fileStream, string fileName, string contentType, CancellationToken ct);
    }
}
