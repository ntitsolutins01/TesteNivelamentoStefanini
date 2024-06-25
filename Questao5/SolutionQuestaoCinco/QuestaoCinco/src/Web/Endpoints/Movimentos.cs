using QuestaoCinco.Application.Movimentos.Commands.CreateMovimento;
using QuestaoCinco.Application.Movimentos.Commands.DeleteMovimento;
using QuestaoCinco.Application.Movimentos.Commands.UpdateMovimento;
using QuestaoCinco.Application.Movimentos.Queries.GetMovimentos;

namespace QuestaoCinco.Web.Endpoints;

public class Movimentos : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            //.RequireAuthorization()
            .MapGet(GetMovimentos)
            .MapPost(CreateMovimento)
            .MapPut(UpdateMovimento, "{id}")
            .MapDelete(DeleteMovimento, "{id}");
    }

    public Task<List<MovimentoDto>> GetMovimentos(ISender sender)
    {
        return  sender.Send(new GetMovimentosQuery());
    }

    public Task<Guid> CreateMovimento(ISender sender, CreateMovimentoCommand command)
    {
        return sender.Send(command);
    }

    public async Task<IResult> UpdateMovimento(ISender sender, Guid id, UpdateMovimentoCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteMovimento(ISender sender, Guid id)
    {
        await sender.Send(new DeleteMovimentoCommand(id));
        return Results.NoContent();
    }
}
