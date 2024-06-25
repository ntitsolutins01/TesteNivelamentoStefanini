namespace QuestaoCinco.Application.ContasCorrentes.Commands.UpdateContaCorrente;

public class UpdateContaCorrenteCommandValidator : AbstractValidator<UpdateContaCorrenteCommand>
{
    public UpdateContaCorrenteCommandValidator()
    {
        RuleFor(v => v.Nome)
            .MaximumLength(100)
            .NotEmpty();
        RuleFor(v => v.Numero)
            .GreaterThan(0)
            .NotEmpty();
    }
}
