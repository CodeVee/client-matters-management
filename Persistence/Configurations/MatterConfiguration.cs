using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class MatterConfiguration : IEntityTypeConfiguration<Matter>
    {
        public void Configure(EntityTypeBuilder<Matter> builder)
        {
            builder.Property(x => x.MatterTitle)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.MatterCode)
                .HasMaxLength(5).IsFixedLength();

            builder.Property(x => x.ClientCode)
                .HasMaxLength(5).IsFixedLength();

            builder.HasAlternateKey(x => x.MatterCode)
                .HasName("AlternateKey_MatterCode");

            builder.HasOne(x => x.Client)
                .WithMany(x => x.Matters)
                .HasForeignKey(x => x.ClientCode)
                .HasPrincipalKey(x => x.ClientCode)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Matters_Clients");
        }
    }
}
