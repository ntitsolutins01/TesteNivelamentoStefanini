using QuestaoCinco.Application.Movimentos.Queries.GetMovimentos;
using QuestaoCinco.Domain.Entities;

namespace QuestaoCinco.Application.FunctionalTests.Movimentos.Queries;

using static Testing;

public class GetMovimentosTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnValorMaiorQueZero()
    {
        await RunAsDefaultUserAsync();

        var query = new GetMovimentosQuery();

        var result = await SendAsync(query);

        result.Count().Should().Be(0);
    }

    [Test]
    public async Task ShouldReturnListaMovimentos()
    {
        await RunAsDefaultUserAsync();

        await AddAsync(new Movimento
        {
            ContaCorrente = new ContaCorrente
            {
                Numero = 123,
                Nome = "Fábio",
                Ativo = true
            },
            TipoMovimento = "I",
            Valor = 10
        });

        var query = new GetMovimentosQuery();

        var result = await SendAsync(query);

        result.Count().Should().BeGreaterThan(1);
    }

    [Test]
    public async Task ShouldDenyAnonymousUser()
    {
        var query = new GetMovimentosQuery();

        var action = () => SendAsync(query);
        
        await action.Should().ThrowAsync<UnauthorizedAccessException>();
    }
}
