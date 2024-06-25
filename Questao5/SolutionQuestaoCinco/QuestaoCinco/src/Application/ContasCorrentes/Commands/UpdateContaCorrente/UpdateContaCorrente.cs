using QuestaoCinco.Application.Common.Interfaces;

namespace QuestaoCinco.Application.ContasCorrentes.Commands.UpdateContaCorrente;

public record UpdateContaCorrenteCommand : IRequest
{
    public required Guid Id { get; init; }
    public required int Numero { get; init; }
    public required string Nome { get; init; }
    public required bool Ativo { get; init; }
}

public class UpdateContaCorrenteCommandHandler : IRequestHandler<UpdateContaCorrenteCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateContaCorrenteCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateContaCorrenteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ContasCorrentes
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Numero = request.Numero;
        entity.Nome = request.Nome;
        entity.Ativo = request.Ativo;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
