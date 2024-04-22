using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThomasGregTest.Domain;

namespace ThomasGregTest.Infrastructure;

public class ClienteConfig : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasMany(e => e.Logradouros)
            .WithOne(logradouro => logradouro.Cliente)
            .HasForeignKey(logradouro => logradouro.ClienteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
