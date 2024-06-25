using QuestaoCinco.Domain.Entities;

namespace QuestaoCinco.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<ContaCorrente> ContasCorrentes { get; }
    DbSet<Movimento> Movimentos { get; }
    DbSet<IdEmpotencia> IdEmpotencias { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
