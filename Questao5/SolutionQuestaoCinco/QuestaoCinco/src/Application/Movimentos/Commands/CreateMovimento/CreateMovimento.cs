using QuestaoCinco.Application.Common.Interfaces;
using QuestaoCinco.Domain.Entities;

namespace QuestaoCinco.Application.Movimentos.Commands.CreateMovimento;

public record CreateMovimentoCommand : IRequest<Guid>
{
    public required Guid ContaCorrenteId { get; init; }
    public DateTime DataMovimento { get; init; }
    public required string TipoMovimento { get; init; }
    public required decimal Valor { get; init; }
}

public class CreateMovimentoCommandHandler : IRequestHandler<CreateMovimentoCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateMovimentoCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateMovimentoCommand request, CancellationToken cancellationToken)
    {
        var contaCorrente = await _context.ContasCorrentes
            .FindAsync(new object[] { request.ContaCorrenteId }, cancellationToken);

        Guard.Against.NotFound(request.ContaCorrenteId, contaCorrente);

        var entity = new Movimento
        {
            ContaCorrente = contaCorrente,
            DataMovimento = request.DataMovimento,
            TipoMovimento = request.TipoMovimento,
            Valor = request.Valor
        };

        _context.Movimentos.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
