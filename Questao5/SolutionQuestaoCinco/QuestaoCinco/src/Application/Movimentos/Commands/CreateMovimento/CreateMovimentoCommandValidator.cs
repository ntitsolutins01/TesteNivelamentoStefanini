using QuestaoCinco.Application.Common.Interfaces;

namespace QuestaoCinco.Application.Movimentos.Commands.CreateMovimento;

public class CreateMovimentoCommandValidator : AbstractValidator<CreateMovimentoCommand>
{
    public CreateMovimentoCommandValidator(IApplicationDbContext context)
    {

        RuleFor(v => v.TipoMovimento)
            .MaximumLength(1)
            .NotEmpty();

        RuleFor(v => v.Valor)
            .GreaterThan(0)
            .NotEmpty();
    }
}
