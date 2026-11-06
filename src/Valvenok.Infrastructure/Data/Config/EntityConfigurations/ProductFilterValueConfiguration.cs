using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Valvenok.Core.Entities.Products;

namespace Valvenok.Infrastructure.Data.Config.EntityConfigurations
{
    public class ProductFilterValueConfiguration : IEntityTypeConfiguration<ProductFilterValue>
    {
        public void Configure(EntityTypeBuilder<ProductFilterValue> builder)
        {
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Value).HasMaxLength(200).IsRequired();

            // Cascade: deleting a product removes its spec values.
            builder.HasOne<CatalogProduct>()
                   .WithMany()
                   .HasForeignKey(v => v.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Restrict on Filter: avoids a second cascade path into this table and blocks
            // deleting a filter that still has values.
            builder.HasOne<Filter>()
                   .WithMany()
                   .HasForeignKey(v => v.FilterId)
                   .OnDelete(DeleteBehavior.Restrict);

            // CategoryId is a denormalised copy (for cheap category-scoped faceting), so it
            // is a plain indexed column with NO foreign key — integrity comes via ProductId.
            builder.HasIndex(v => new { v.CategoryId, v.FilterId });

            // Powers faceted filtering: "give me all values for filter X".
            builder.HasIndex(v => new { v.FilterId, v.Value });

            builder.HasIndex(v => v.ProductId);
        }
    }
}
