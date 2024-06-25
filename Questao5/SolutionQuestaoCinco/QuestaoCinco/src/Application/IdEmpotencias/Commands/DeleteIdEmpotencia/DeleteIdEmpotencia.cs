using QuestaoCinco.Application.Common.Interfaces;

namespace QuestaoCinco.Application.IdEmpotencias.Commands.DeleteIdEmpotencia;

public record DeleteIdEmpotenciaCommand(Guid Id) : IRequest;

public class DeleteIdEmpotenciaCommandHandler : IRequestHandler<DeleteIdEmpotenciaCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteIdEmpotenciaCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteIdEmpotenciaCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.IdEmpotencias
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.IdEmpotencias.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
