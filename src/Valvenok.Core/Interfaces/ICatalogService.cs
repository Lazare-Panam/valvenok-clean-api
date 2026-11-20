using Valvenok.Core.Common.Errors;
using Valvenok.Core.Entities.Content;

namespace Valvenok.Core.Interfaces
{
    public interface ICatalogService
    {
        Task<Result<CatalogPageResponse>> GetPageAsync(string path, CancellationToken ct = default);
    }
}
