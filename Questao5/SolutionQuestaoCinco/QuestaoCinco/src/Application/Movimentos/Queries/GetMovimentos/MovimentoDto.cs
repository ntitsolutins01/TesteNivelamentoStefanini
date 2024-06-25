using QuestaoCinco.Domain.Entities;

namespace QuestaoCinco.Application.Movimentos.Queries.GetMovimentos;

public class MovimentoDto
{
    public Guid Id { get; init; }
    public required Guid ContaCorrenteId { get; set; }
    public DateTime DataMovimento { get; set; }
    public required string TipoMovimento { get; set; }
    public required decimal Valor { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Movimento, MovimentoDto>();
        }
    }
}
