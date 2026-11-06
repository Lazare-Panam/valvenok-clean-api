using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Valvenok.Core.Entities.Content;
using Valvenok.Core.Interfaces.Repositories;

namespace Valvenok.Infrastructure.Repository
{
    public class CategoryContentRepository : ICategoryContentRepository
    {
        // Mongo collection that stores the CategoryContent documents.
        private const string CollectionName = "categoryContent";

        private readonly IMongoCollection<CategoryContent> _collection;
        private readonly ILogger<CategoryContentRepository> _logger;

        public CategoryContentRepository(IMongoDatabase database, ILogger<CategoryContentRepository> logger)
        {
            _collection = database.GetCollection<CategoryContent>(CollectionName);
            _logger = logger;
        }

        public async Task<CategoryContent?> GetByCategoryIdAsync(int categoryId, CancellationToken ct = default)
        {
            try
            {
                return await _collection.Find(x => x.CategoryId == categoryId).FirstOrDefaultAsync(ct);
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogDebug(ex, "Operation cancelled fetching {EntityType} {CategoryId}", nameof(CategoryContent), categoryId);
                throw;
            }
            catch (MongoException ex)
            {
                _logger.LogWarning(ex, "MongoDB error fetching {EntityType} {CategoryId}", nameof(CategoryContent), categoryId);
                throw;
            }
        }

    }
}
