using QuestaoCinco.Domain.Entities;

namespace QuestaoCinco.Application.IdEmpotencias.Queries.GetIdEmpotencias;

public class IdEmpotenciaDto
{
    public Guid Id { get; init; }
    public string? Requisicao { get; init; }
    public string? Resultado { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<IdEmpotencia, IdEmpotenciaDto>();
        }
    }
}
