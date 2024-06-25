using QuestaoCinco.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QuestaoCinco.Infrastructure.Data.Configurations;

public class MovimentoConfiguration : IEntityTypeConfiguration<Movimento>
{
    public void Configure(EntityTypeBuilder<Movimento> builder)
    {
        builder.Property(t => t.DataMovimento)
            .IsRequired();
        builder.Property(t => t.TipoMovimento)
            .HasMaxLength(1)
            .IsRequired();
        builder.Property(t => t.Valor)
            .HasPrecision(10,2)
            .IsRequired();
    }
}
