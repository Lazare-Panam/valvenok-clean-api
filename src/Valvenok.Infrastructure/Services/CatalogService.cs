using Valvenok.Core.Common.Errors;
using Valvenok.Core.Entities.Content;
using Valvenok.Core.Interfaces;
using Valvenok.Core.Interfaces.Repositories;

namespace Valvenok.Infrastructure.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly ICatalogPageRepository _catalogPages;

        public CatalogService(ICatalogPageRepository catalogPages)
        {
            _catalogPages = catalogPages;
        }

        public async Task<Result<CatalogPageResponse>> GetPageAsync(string path, CancellationToken ct = default)
        {

            // Normalise to match stored paths (exact lookup): drop surrounding slashes and
            // lowercase (invariant — avoids the Turkish-İ bug) so "/Actuators/" == "actuators".
            path = path.Trim('/').ToLowerInvariant();

            var page = await _catalogPages.GetByPathAsync(path, ct);
            if (page is null)
            {
                return CatalogErrors.NotFound(path);
            }

            var children = await _catalogPages.GetChildrenAsync(path, ct);
            return new CatalogPageResponse(page, children);
        }
    }
}
