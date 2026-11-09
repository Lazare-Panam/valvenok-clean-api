using System;
using System.Collections.Generic;
using System.Text;
using Valvenok.Core.Entities.Content;

namespace Valvenok.Core.Interfaces.Repositories
{
    public interface ICategoryContentRepository
    {
        Task<CategoryContent?> GetByCategoryIdAsync(int categoryID, CancellationToken ct = default);
    }
}
