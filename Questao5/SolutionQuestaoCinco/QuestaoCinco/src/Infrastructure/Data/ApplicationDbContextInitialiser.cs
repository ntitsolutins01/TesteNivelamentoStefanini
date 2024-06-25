using System.Runtime.InteropServices;
using QuestaoCinco.Domain.Constants;
using QuestaoCinco.Domain.Entities;
using QuestaoCinco.Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace QuestaoCinco.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var administratorRole = new IdentityRole(Roles.Administrator);

        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
        }

        // Default users
        var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "Administrator1!");
            if (!string.IsNullOrWhiteSpace(administratorRole.Name))
            {
                await _userManager.AddToRolesAsync(administrator, new [] { administratorRole.Name });
            }
        }

        //Default data
        if (!_context.ContasCorrentes.Any())
        {
            _context.ContasCorrentes.Add(new ContaCorrente()
            {
                Id = new Guid("B6BAFC09-6967-ED11-A567-055DFA4A16C9"),
                Numero = 123,
                Nome = "Katherine Sanchez",
                Ativo = true
            });
            _context.ContasCorrentes.Add(new ContaCorrente()
            {
                Id = new Guid("FA99D033-7067-ED11-96C6-7C5DFA4A16C9"),
                Numero = 456,
                Nome = "Eva Woodward",
                Ativo = true
            });
            _context.ContasCorrentes.Add(new ContaCorrente()
            {
                Id = new Guid("382D323D-7067-ED11-8866-7D5DFA4A16C9"),
                Numero = 789,
                Nome = "Tevin Mcconnell",
                Ativo = true
            });
            _context.ContasCorrentes.Add(new ContaCorrente()
            {
                Id = new Guid("F475F943-7067-ED11-A06B-7E5DFA4A16C9"),
                Numero = 741,
                Nome = "Ameena Lynn",
                Ativo = false
            });
            _context.ContasCorrentes.Add(new ContaCorrente()
            {
                Id = new Guid("BCDACA4A-7067-ED11-AF81-825DFA4A16C9"),
                Numero = 852,
                Nome = "Jarrad Mckee",
                Ativo = false
            });
            _context.ContasCorrentes.Add(new ContaCorrente()
            {
                Id = new Guid("D2E02051-7067-ED11-94C0-835DFA4A16C9"),
                Numero = 963,
                Nome = "Elisha Simons",
                Ativo = false
            });

            await _context.SaveChangesAsync();
        }
    }
}
