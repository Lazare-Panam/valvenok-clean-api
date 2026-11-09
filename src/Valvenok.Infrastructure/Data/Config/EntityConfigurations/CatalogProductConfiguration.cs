using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Valvenok.Core.Entities.Products;

namespace Valvenok.Infrastructure.Data.Config.EntityConfigurations
{
    public class CatalogProductConfiguration : IEntityTypeConfiguration<CatalogProduct>
    {
        public void Configure(EntityTypeBuilder<CatalogProduct> builder)
        {
            builder.HasKey(p => p.CatalogProductId);

            builder.Property(p => p.PartNumber).HasMaxLength(100).IsRequired();
            builder.HasIndex(p => p.PartNumber).IsUnique();

            builder.Property(p => p.ProductStatus).HasMaxLength(50);
            builder.Property(p => p.ImageUrl).HasMaxLength(2048);
            builder.Property(p => p.Price).HasPrecision(18, 2);
            builder.HasOne<Category>()
                   .WithMany()
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<Brands>()
                   .WithMany()
                   .HasForeignKey(p => p.BrandId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(p => p.CategoryId);
            builder.HasIndex(p => p.BrandId);
        }
    }
}
