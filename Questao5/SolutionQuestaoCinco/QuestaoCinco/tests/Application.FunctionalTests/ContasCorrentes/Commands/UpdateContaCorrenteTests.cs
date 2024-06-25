using QuestaoCinco.Application.ContasCorrentes.Commands.CreateContaCorrente;
using QuestaoCinco.Application.ContasCorrentes.Commands.UpdateContaCorrente;
using QuestaoCinco.Domain.Entities;

namespace QuestaoCinco.Application.FunctionalTests.ContasCorrentes.Commands;

using static Testing;

public class UpdateContaCorrenteTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidContaCorrenteId()
    {
        var command = new UpdateContaCorrenteCommand { Id = new Guid(), Nome = "Fábio", Numero = 123, Ativo = false};
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldUpdateContaCorrente()
    {
        var userId = await RunAsDefaultUserAsync();

        var itemId = await SendAsync(new CreateContaCorrenteCommand
        {
            Numero = 789,
            Nome = "Souza",
            Ativo = true
        });

        var command = new UpdateContaCorrenteCommand
        {
            Id = itemId,
            Numero = 987,
            Nome = "Azuos",
            Ativo = false
        };

        await SendAsync(command);

        var item = await FindAsync<ContaCorrente>(itemId);

        item.Should().NotBeNull();
        item!.Nome.Should().Be(command.Nome);
        item.LastModifiedBy.Should().NotBeNull();
        item.LastModifiedBy.Should().Be(userId);
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
