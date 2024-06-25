using QuestaoCinco.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QuestaoCinco.Infrastructure.Data.Configurations;

public class ContaCorrenteConfiguration : IEntityTypeConfiguration<ContaCorrente>
{
    public void Configure(EntityTypeBuilder<ContaCorrente> builder)
    {
        builder.Property(t => t.Nome)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(t => t.Numero)
            .HasMaxLength(10)
            .IsRequired();
        builder.Property(t => t.Ativo)
            .HasDefaultValue(true)
            .IsRequired();
    }
}
