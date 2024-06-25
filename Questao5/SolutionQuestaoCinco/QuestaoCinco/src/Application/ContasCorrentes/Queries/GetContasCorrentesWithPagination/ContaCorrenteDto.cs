using QuestaoCinco.Domain.Entities;

namespace QuestaoCinco.Application.ContasCorrentes.Queries.GetContasCorrentesWithPagination;

public class ContaCorrenteDto
{
    public Guid Id { get; init; }
    public required int Numero { get; init; }
    public required string Nome { get; init; }
    public required bool Ativo { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ContaCorrente, ContaCorrenteDto>();
        }
    }
}
