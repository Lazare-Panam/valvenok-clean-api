using Microsoft.AspNetCore.Mvc;
using Valvenok.Api.Extensions;
using Valvenok.Core.Interfaces;

namespace Valvenok.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogService _catalogService;
        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpGet("{*path}")]
        public async Task<IActionResult> GetByPath(string path, CancellationToken ct)
        {
            // No logging here: anonymous GET, no actor to audit — request logging already
            // records it, and the service logs the lookup outcome.
            var result = await _catalogService.GetPageAsync(path, ct);
            return result.ToActionResult();
        }
    }
}
