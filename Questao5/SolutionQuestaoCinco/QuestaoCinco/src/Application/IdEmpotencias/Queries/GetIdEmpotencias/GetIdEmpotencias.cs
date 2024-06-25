using QuestaoCinco.Application.Common.Interfaces;
using QuestaoCinco.Application.Common.Security;

namespace QuestaoCinco.Application.IdEmpotencias.Queries.GetIdEmpotencias;

[Authorize]
public record GetIdEmpotenciasQuery : IRequest<List<IdEmpotenciaDto>>;

public class GetIdEmpotenciasQueryHandler : IRequestHandler<GetIdEmpotenciasQuery, List<IdEmpotenciaDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetIdEmpotenciasQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<IdEmpotenciaDto>> Handle(GetIdEmpotenciasQuery request, CancellationToken cancellationToken)
    {
        return await _context.IdEmpotencias
            .AsNoTracking()
            .ProjectTo<IdEmpotenciaDto>(_mapper.ConfigurationProvider)
            .OrderBy(t => t.Id)
            .ToListAsync(cancellationToken);
    }
}
