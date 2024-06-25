namespace QuestaoCinco.Application.ContasCorrentes.Commands.CreateContaCorrente;

public class CreateContaCorrenteCommandValidator : AbstractValidator<CreateContaCorrenteCommand>
{
    public CreateContaCorrenteCommandValidator()
    {
        RuleFor(v => v.Nome)
            .MaximumLength(100)
            .NotEmpty();
        RuleFor(v => v.Numero)
            .GreaterThan(0)
            .NotEmpty();
    }   
}
