using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Valvenok.Core.Entities.Content;
using Valvenok.Core.Interfaces.Repositories;

namespace Valvenok.Infrastructure.Repository
{
    public class CategoryContentRepository : ICategoryContentRepository
    {
        // Mongo collection that stores the CategoryContent documents.
        private const string CollectionName = "category-content";
        private readonly IMongoCollection<CategoryContent> _collection;

        public CategoryContentRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<CategoryContent>(CollectionName);
        }

        public async Task<CategoryContent?> GetByCategoryIdAsync(int categoryId, CancellationToken ct = default)
        {
           return await _collection.Find(x => x.CategoryId == categoryId).FirstOrDefaultAsync(ct);
        }
    }
}
