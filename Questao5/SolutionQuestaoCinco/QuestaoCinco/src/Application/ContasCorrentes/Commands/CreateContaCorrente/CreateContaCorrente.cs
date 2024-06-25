using QuestaoCinco.Application.Common.Interfaces;
using QuestaoCinco.Domain.Entities;

namespace QuestaoCinco.Application.ContasCorrentes.Commands.CreateContaCorrente;

public record CreateContaCorrenteCommand : IRequest<Guid>
{
    public required int Numero { get; init; }
    public required string Nome { get; init; }
    public required bool Ativo { get; init; }
}

public class CreateContaCorrenteCommandHandler : IRequestHandler<CreateContaCorrenteCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateContaCorrenteCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateContaCorrenteCommand request, CancellationToken cancellationToken)
    {
        var entity = new ContaCorrente
        {
            Numero = request.Numero,
            Nome = request.Nome,
            Ativo = request.Ativo
        };

        _context.ContasCorrentes.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
