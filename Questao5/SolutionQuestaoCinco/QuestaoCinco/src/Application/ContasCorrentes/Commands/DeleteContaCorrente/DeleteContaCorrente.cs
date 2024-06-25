using QuestaoCinco.Application.Common.Interfaces;

namespace QuestaoCinco.Application.ContasCorrentes.Commands.DeleteContaCorrente;

public record DeleteContaCorrenteCommand(Guid Id) : IRequest;

public class DeleteContaCorrenteCommandHandler : IRequestHandler<DeleteContaCorrenteCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteContaCorrenteCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteContaCorrenteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ContasCorrentes
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.ContasCorrentes.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }

}
