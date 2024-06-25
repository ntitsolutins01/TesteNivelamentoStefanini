using System.Reflection;
using QuestaoCinco.Application.Common.Interfaces;
using QuestaoCinco.Domain.Entities;
using QuestaoCinco.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace QuestaoCinco.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<ContaCorrente> ContasCorrentes => Set<ContaCorrente>();
    public DbSet<Movimento> Movimentos => Set<Movimento>();
    public DbSet<IdEmpotencia> IdEmpotencias => Set<IdEmpotencia>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
