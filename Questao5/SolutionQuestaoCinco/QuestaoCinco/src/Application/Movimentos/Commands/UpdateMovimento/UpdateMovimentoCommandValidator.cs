using QuestaoCinco.Application.Common.Interfaces;

namespace QuestaoCinco.Application.Movimentos.Commands.UpdateMovimento;

public class UpdateMovimentoCommandValidator : AbstractValidator<UpdateMovimentoCommand>
{

    public UpdateMovimentoCommandValidator(IApplicationDbContext context)
    {
        RuleFor(v => v.TipoMovimento)
            .MaximumLength(1)
            .NotEmpty();

        RuleFor(v => v.Valor)
            .GreaterThan(0)
            .NotEmpty();
    }
}
