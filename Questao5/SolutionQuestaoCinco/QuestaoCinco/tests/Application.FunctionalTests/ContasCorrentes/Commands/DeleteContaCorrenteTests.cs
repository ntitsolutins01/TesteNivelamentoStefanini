using QuestaoCinco.Application.ContasCorrentes.Commands.CreateContaCorrente;
using QuestaoCinco.Application.ContasCorrentes.Commands.DeleteContaCorrente;
using QuestaoCinco.Domain.Entities;

namespace QuestaoCinco.Application.FunctionalTests.ContasCorrentes.Commands;

using static Testing;

public class DeleteContaCorrenteTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidContaCorrenteId()
    {
        var command = new DeleteContaCorrenteCommand(new Guid());

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteContaCorrente()
    {

        var itemId = await SendAsync(new CreateContaCorrenteCommand
        {
            Numero = 321,
            Nome = "Muniz",
            Ativo = true
        });

        await SendAsync(new DeleteContaCorrenteCommand(itemId));

        var item = await FindAsync<ContaCorrente>(itemId);

        item.Should().BeNull();
    }
}
