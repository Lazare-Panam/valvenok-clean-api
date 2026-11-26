using Microsoft.Extensions.Logging;
using Valvenok.Core.Common.Errors;
using Valvenok.Core.Entities.Content;
using Valvenok.Core.Interfaces;
using Valvenok.Core.Interfaces.Repositories;

namespace Valvenok.Infrastructure.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly ICatalogPageRepository _catalogPages;
        private readonly ILogger<CatalogService> _logger;
        public CatalogService(ICatalogPageRepository catalogPages, ILogger<CatalogService> logger)
        {
            _catalogPages = catalogPages;
            _logger = logger;
        }

        public async Task<Result<CatalogPageResponse>> GetPageAsync(string path, CancellationToken ct = default)
        {

            // Normalise to match stored paths (exact lookup): drop surrounding slashes and
            // lowercase (invariant — avoids the Turkish-İ bug) so "/Actuators/" == "actuators".
            path = path.Trim('/').ToLowerInvariant();
            _logger.LogDebug("Looking up catalog page {Path}", path);
            var page = await _catalogPages.GetByPathAsync(path, ct);
            if (page is null)
            {
                // A missing page is a normal 404 (bots, dead links) — Debug, not Warning/Error.
                _logger.LogDebug("Catalog page not found for {Path}", path);
                return CatalogErrors.NotFound(path);
            }

            var children = await _catalogPages.GetChildrenAsync(path, ct);
            return new CatalogPageResponse(page, children);
        }
    }
}
