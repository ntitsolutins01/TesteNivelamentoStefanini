using QuestaoCinco.Application.Movimentos.Commands.CreateMovimento;
using QuestaoCinco.Application.Movimentos.Commands.DeleteMovimento;
using QuestaoCinco.Domain.Entities;

namespace QuestaoCinco.Application.FunctionalTests.Movimentos.Commands;

using static Testing;

public class DeleteMovimentoTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidMovimentoId()
    {
        var command = new DeleteMovimentoCommand(new Guid());
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteMovimento()
    {
        var movimento = await SendAsync(new CreateMovimentoCommand
        {
            ContaCorrenteId = new Guid(),
            TipoMovimento = "C",
            Valor = 200
        });

        await SendAsync(new DeleteMovimentoCommand(movimento));

        var list = await FindAsync<Movimento>(movimento);

        list.Should().BeNull();
    }
}
