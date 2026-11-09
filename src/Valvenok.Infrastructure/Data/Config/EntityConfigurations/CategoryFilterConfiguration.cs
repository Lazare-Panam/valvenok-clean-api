using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Valvenok.Core.Entities.Products;

namespace Valvenok.Infrastructure.Data.Config.EntityConfigurations
{
    public class CategoryFilterConfiguration : IEntityTypeConfiguration<CategoryFilter>
    {
        public void Configure(EntityTypeBuilder<CategoryFilter> builder)
        {
            builder.HasKey(cf => new { cf.CategoryId, cf.FilterId });

            builder.HasOne<Category>()
                   .WithMany()
                   .HasForeignKey(cf => cf.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Filter>()
                   .WithMany()
                   .HasForeignKey(cf => cf.FilterId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
