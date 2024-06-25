using QuestaoCinco.Application.Common.Models;
using QuestaoCinco.Application.ContasCorrentes.Commands.DeleteContaCorrente;
using QuestaoCinco.Application.ContasCorrentes.Commands.UpdateContaCorrente;
using QuestaoCinco.Application.ContasCorrentes.Queries.GetContasCorrentesWithPagination;
using QuestaoCinco.Application.ContasCorrentes.Commands.CreateContaCorrente;

namespace QuestaoCinco.Web.Endpoints;

public class ContasCorrentes : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            //.RequireAuthorization()
            .MapGet(GetContasCorrentesWithPagination)
            .MapPost(CreateContaCorrente)
            .MapPut(UpdateContaCorrente, "{id}")
            .MapDelete(DeleteContaCorrente, "{id}");
    }

    public Task<PaginatedList<ContaCorrenteDto>> GetContasCorrentesWithPagination(ISender sender, [AsParameters] GetContasCorrentesWithPaginationQuery query)
    {
        return sender.Send(query);
    }

    public Task<Guid> CreateContaCorrente(ISender sender, CreateContaCorrenteCommand command)
    {
        return sender.Send(command);
    }

    public async Task<IResult> UpdateContaCorrente(ISender sender, Guid id, UpdateContaCorrenteCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }


    public async Task<IResult> DeleteContaCorrente(ISender sender, Guid id)
    {
        await sender.Send(new DeleteContaCorrenteCommand(id));
        return Results.NoContent();
    }
}
