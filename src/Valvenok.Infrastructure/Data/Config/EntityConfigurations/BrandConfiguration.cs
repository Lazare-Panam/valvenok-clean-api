using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Valvenok.Core.Entities.Products;

namespace Valvenok.Infrastructure.Data.Config.EntityConfigurations
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brands>
    {
        public void Configure(EntityTypeBuilder<Brands> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.LogoUrl)
                .HasMaxLength(2048);

            builder.Property(p => p.WebsiteLink)
                .IsRequired(false);
        }
    }
}
