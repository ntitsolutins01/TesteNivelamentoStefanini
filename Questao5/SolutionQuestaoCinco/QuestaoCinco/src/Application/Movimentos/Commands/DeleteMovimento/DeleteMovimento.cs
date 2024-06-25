using QuestaoCinco.Application.Common.Interfaces;

namespace QuestaoCinco.Application.Movimentos.Commands.DeleteMovimento;

public record DeleteMovimentoCommand(Guid Id) : IRequest;

public class DeleteMovimentoCommandHandler : IRequestHandler<DeleteMovimentoCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteMovimentoCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteMovimentoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Movimentos
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.Movimentos.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
