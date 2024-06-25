namespace QuestaoCinco.Application.ContasCorrentes.Queries.GetContasCorrentesWithPagination;

public class GetContasCorrentesWithPaginationQueryValidator : AbstractValidator<GetContasCorrentesWithPaginationQuery>
{
    public GetContasCorrentesWithPaginationQueryValidator()
    {

        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber pelo menos maior ou igual a 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize pelo menos maior ou igual a 1.");
    }
}
