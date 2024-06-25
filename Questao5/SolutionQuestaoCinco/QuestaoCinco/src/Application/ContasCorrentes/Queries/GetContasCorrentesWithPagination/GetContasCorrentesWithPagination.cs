using QuestaoCinco.Application.Common.Interfaces;
using QuestaoCinco.Application.Common.Mappings;
using QuestaoCinco.Application.Common.Models;

namespace QuestaoCinco.Application.ContasCorrentes.Queries.GetContasCorrentesWithPagination;

public record GetContasCorrentesWithPaginationQuery : IRequest<PaginatedList<ContaCorrenteDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetContasCorrentesWithPaginationQueryHandler : IRequestHandler<GetContasCorrentesWithPaginationQuery, PaginatedList<ContaCorrenteDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetContasCorrentesWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ContaCorrenteDto>> Handle(GetContasCorrentesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.ContasCorrentes
            .OrderBy(x => x.Nome)
            .ProjectTo<ContaCorrenteDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
