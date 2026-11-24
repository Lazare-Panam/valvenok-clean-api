using MongoDB.Driver;
using Valvenok.Core.Entities.Content;
using Valvenok.Core.Interfaces.Repositories;

namespace Valvenok.Infrastructure.Repository
{
    public class CatalogPageRepository : ICatalogPageRepository
    {
        private const string CollectionName = "category-content";
        private readonly IMongoCollection<CatalogPage> _collection;

        public CatalogPageRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<CatalogPage>(CollectionName);
        }

        public async Task<CatalogPage?> GetByPathAsync(string path, CancellationToken ct = default)
        {
            return await _collection.Find(x => x.Path == path).FirstOrDefaultAsync(ct);
        }

        public async Task<List<CatalogPage>> GetChildrenAsync(string parentPath, CancellationToken ct = default)
        {
            return await _collection.Find(x => x.ParentPath == parentPath).SortBy(x => x.Order).ToListAsync(ct);
        }
    }
}
