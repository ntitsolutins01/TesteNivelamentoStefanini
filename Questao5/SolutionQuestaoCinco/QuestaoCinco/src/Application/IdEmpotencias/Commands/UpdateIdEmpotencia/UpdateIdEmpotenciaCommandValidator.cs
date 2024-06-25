using QuestaoCinco.Application.Common.Interfaces;

namespace QuestaoCinco.Application.IdEmpotencias.Commands.UpdateIdEmpotencia;

public class UpdateIdEmpotenciaCommandValidator : AbstractValidator<UpdateIdEmpotenciaCommand>
{

    public UpdateIdEmpotenciaCommandValidator(IApplicationDbContext context)
    {
        RuleFor(v => v.Requisicao)
            .MaximumLength(1000);
        RuleFor(v => v.Resultado)
            .MaximumLength(1000);
    }
}
