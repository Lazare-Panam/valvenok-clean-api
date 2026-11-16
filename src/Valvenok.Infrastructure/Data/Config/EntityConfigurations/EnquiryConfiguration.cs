using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Valvenok.Core.Entities.User;

namespace Valvenok.Infrastructure.Data.Config.EntityConfigurations
{
    public class EnquiryConfiguration : IEntityTypeConfiguration<Enquiry>
    {
        public void Configure(EntityTypeBuilder<Enquiry> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Subject).HasMaxLength(150).IsRequired();
            builder.Property(p => p.Message).HasMaxLength(500).IsRequired();
            builder.Property(p => p.PhoneNo).HasMaxLength(20).IsRequired();
            builder.Property(p => p.Email).HasMaxLength(254).IsRequired();
            builder.Property(e => e.Status).HasConversion<string>().HasMaxLength(20);
            builder.Property(e => e.ExpectedLeadTime).HasMaxLength(200);
        }
    }
}
