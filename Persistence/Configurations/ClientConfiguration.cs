using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.Property(x => x.ClientName)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(x => x.ClientCode)
                .HasMaxLength(5).IsFixedLength();

            builder.HasAlternateKey(x => x.ClientCode)
                .HasName("AlternateKey_ClientCode");
        }
    }
}
