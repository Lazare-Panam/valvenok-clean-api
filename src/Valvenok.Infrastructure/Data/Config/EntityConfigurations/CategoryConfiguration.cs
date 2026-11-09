using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Valvenok.Core.Entities.Products;

namespace Valvenok.Infrastructure.Data.Config.EntityConfigurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.CategoryId);
            builder.Property(c => c.Name).HasMaxLength(200).IsRequired();
            builder.Property(c => c.Slug).HasMaxLength(200).IsRequired();
            builder.Property(c => c.Path).HasMaxLength(450).IsRequired();
            builder.HasIndex(c => c.Path).IsUnique();
            builder.HasOne<Category>()
                   .WithMany()
                   .HasForeignKey(c => c.ParentId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
