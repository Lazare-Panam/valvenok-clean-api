using Valvenok.Core.Entities.Content;

namespace Valvenok.Core.Interfaces.Repositories
{
    public interface ICategoryContentRepository
    {
        Task<CategoryContent?> GetByCategoryIdAsync(int categoryID, CancellationToken ct = default);
    }
}
