using System;
using System.Collections.Generic;
using System.Text;

namespace Valvenok.Core.Interfaces
{
    public interface IFileStorage
    {
        Task<string> UploadFile(Stream fileStream, string fileName, string contentType, CancellationToken ct);
    }
}
