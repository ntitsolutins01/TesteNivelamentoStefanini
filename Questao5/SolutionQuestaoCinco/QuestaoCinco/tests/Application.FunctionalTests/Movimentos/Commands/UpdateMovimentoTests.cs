using QuestaoCinco.Application.Movimentos.Commands.CreateMovimento;
using QuestaoCinco.Application.Movimentos.Commands.UpdateMovimento;
using QuestaoCinco.Domain.Entities;

namespace QuestaoCinco.Application.FunctionalTests.Movimentos.Commands;

using static Testing;

public class UpdateMovimentoTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidMovimentoId()
    {
        var command = new UpdateMovimentoCommand
        {
            Id = new Guid(),
            Valor = 200,
            TipoMovimento = "C",
            ContaCorrenteId = new Guid(),
            DataMovimento = DateTime.Now
        };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldUpdateMovimento()
    {
        var userId = await RunAsDefaultUserAsync();

        var movimento = await SendAsync(new CreateMovimentoCommand
        {
            ContaCorrenteId = new Guid(),
            TipoMovimento = "I",
            Valor = 30
        });

        var command = new UpdateMovimentoCommand
        {
            Id = new Guid(),
            ContaCorrenteId = new Guid(),
            TipoMovimento = "C",
            Valor = 20
        };

        await SendAsync(command);

        var list = await FindAsync<Movimento>(movimento);

        list.Should().NotBeNull();
        list!.Valor.Should().Be(command.Valor);
        list.LastModifiedBy.Should().NotBeNull();
        list.LastModifiedBy.Should().Be(userId);
        list.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
