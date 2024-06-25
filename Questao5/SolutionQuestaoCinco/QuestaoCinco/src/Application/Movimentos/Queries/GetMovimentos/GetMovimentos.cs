using QuestaoCinco.Application.Common.Interfaces;
using QuestaoCinco.Application.Common.Security;

namespace QuestaoCinco.Application.Movimentos.Queries.GetMovimentos;

[Authorize]
public record GetMovimentosQuery : IRequest<List<MovimentoDto>>;

public class GetMovimentosQueryHandler : IRequestHandler<GetMovimentosQuery, List<MovimentoDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetMovimentosQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<MovimentoDto>> Handle(GetMovimentosQuery request, CancellationToken cancellationToken)
    {
        return await _context.Movimentos
            .AsNoTracking()
            .ProjectTo<MovimentoDto>(_mapper.ConfigurationProvider)
            .OrderBy(t => t.Id)
            .ToListAsync(cancellationToken);
    }
}
