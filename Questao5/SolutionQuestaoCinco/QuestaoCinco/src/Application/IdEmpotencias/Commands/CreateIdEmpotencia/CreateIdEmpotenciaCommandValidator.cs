using QuestaoCinco.Application.Common.Interfaces;

namespace QuestaoCinco.Application.IdEmpotencias.Commands.CreateIdEmpotencia;

public class CreateIdEmpotenciaCommandValidator : AbstractValidator<CreateIdEmpotenciaCommand>
{
    public CreateIdEmpotenciaCommandValidator(IApplicationDbContext context)
    {

        RuleFor(v => v.Requisicao)
            .MaximumLength(1000);
        RuleFor(v => v.Resultado)
            .MaximumLength(1000);
    }
}
