using QuestaoCinco.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QuestaoCinco.Infrastructure.Data.Configurations;

public class IdEmpotenciaConfiguration : IEntityTypeConfiguration<IdEmpotencia>
{
    public void Configure(EntityTypeBuilder<IdEmpotencia> builder)
    {
        builder.Property(t => t.Requisicao)
            .HasMaxLength(1000);
        builder.Property(t => t.Resultado)
            .HasMaxLength(1000);
    }
}
