using QuestaoCinco.Application.ContasCorrentes.Commands.CreateContaCorrente;
using QuestaoCinco.Domain.Entities;

namespace QuestaoCinco.Application.FunctionalTests.ContasCorrentes.Commands;

using static Testing;

public class CreateContaCorrenteTests : BaseTestFixture
{
    [Test]
    public async Task ShouldCreateContaCorrente()
    {
        var userId = await RunAsDefaultUserAsync();


        var command = new CreateContaCorrenteCommand
        {
            Numero = 123,
            Nome = "Fábio",
            Ativo = false
        };

        var itemId = await SendAsync(command);

        var item = await FindAsync<ContaCorrente>(itemId);

        item.Should().NotBeNull();
        item!.Nome.Should().Be(command.Nome);
        item.Numero.Should().Be(command.Numero);
        item.CreatedBy.Should().Be(userId);
        item.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        item.LastModifiedBy.Should().Be(userId);
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
