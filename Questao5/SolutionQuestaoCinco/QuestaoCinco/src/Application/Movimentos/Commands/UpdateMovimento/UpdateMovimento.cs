using QuestaoCinco.Application.Common.Interfaces;

namespace QuestaoCinco.Application.Movimentos.Commands.UpdateMovimento;

public record UpdateMovimentoCommand : IRequest
{
    public required Guid Id { get; init; }
    public required Guid ContaCorrenteId { get; init; }
    public DateTime DataMovimento { get; init; }
    public required string TipoMovimento { get; init; }
    public required decimal Valor { get; init; }
}

public class UpdateMovimentoCommandHandler : IRequestHandler<UpdateMovimentoCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateMovimentoCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateMovimentoCommand request, CancellationToken cancellationToken)
    {
        var contaCorrente = await _context.ContasCorrentes
            .FindAsync(new object[] { request.ContaCorrenteId }, cancellationToken);

        Guard.Against.NotFound(request.ContaCorrenteId, contaCorrente);

        var entity = await _context.Movimentos
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.ContaCorrente = contaCorrente;
        entity.DataMovimento = request.DataMovimento;
        entity.TipoMovimento = request.TipoMovimento;
        entity.Valor = request.Valor;

        await _context.SaveChangesAsync(cancellationToken);

    }
}
