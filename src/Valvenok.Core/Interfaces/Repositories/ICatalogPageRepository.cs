using Valvenok.Core.Entities.Content;

namespace Valvenok.Core.Interfaces.Repositories
{
    public interface ICatalogPageRepository
    {
        Task<CatalogPage?> GetByPathAsync(string path, CancellationToken ct = default);
        Task<List<CatalogPage>> GetChildrenAsync(string parentPath, CancellationToken ct = default);
    }
}
