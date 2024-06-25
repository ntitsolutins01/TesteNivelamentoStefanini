using QuestaoCinco.Application.Common.Exceptions;
using QuestaoCinco.Application.Movimentos.Commands.CreateMovimento;
using QuestaoCinco.Domain.Entities;

namespace QuestaoCinco.Application.FunctionalTests.Movimentos.Commands;

using static Testing;

public class CreateMovimentoTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateMovimentoCommand
        {
            ContaCorrenteId = default,
            TipoMovimento = null!,
            Valor = 0
        };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldRequireUniqueTitle()
    {
        await SendAsync(new CreateMovimentoCommand
        {
            TipoMovimento = "I",
            ContaCorrenteId = default,
            Valor = 0
        });

        var command = new CreateMovimentoCommand
        {
            ContaCorrenteId = default,
            TipoMovimento = "C",
            Valor = 0
        };

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateMovimento()
    {
        var userId = await RunAsDefaultUserAsync();

        var command = new CreateMovimentoCommand
        {
            ContaCorrenteId = new Guid(),
            TipoMovimento = "C",
            Valor = 10
        };

        var id = await SendAsync(command);

        var list = await FindAsync<Movimento>(id);

        list.Should().NotBeNull();
        list!.Valor.Should().Be(command.Valor);
        list.CreatedBy.Should().Be(userId);
        list.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
