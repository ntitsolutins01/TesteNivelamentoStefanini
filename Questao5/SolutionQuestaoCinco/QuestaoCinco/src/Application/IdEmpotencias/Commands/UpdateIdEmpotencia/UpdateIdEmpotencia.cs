using System.Text;
using Dapper;
using QuestaoCinco.Application.Common.Interfaces;

namespace QuestaoCinco.Application.IdEmpotencias.Commands.UpdateIdEmpotencia;

public record UpdateIdEmpotenciaCommand : IRequest<bool>
{
    public required Guid Id { get; init; }
    public string? Requisicao { get; init; }
    public string? Resultado { get; init; }
}

public class UpdateIdEmpotenciaCommandHandler : IRequestHandler<UpdateIdEmpotenciaCommand, bool>
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public UpdateIdEmpotenciaCommandHandler(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<bool> Handle(UpdateIdEmpotenciaCommand request, CancellationToken cancellationToken)
    {

        var sql = new StringBuilder(@"UPDATE IdEmpotencia SET Requisicao = @Requisicao, Resultado = @Resultado, LastModified = getdate() WHERE Id = @Id");

        var idEmpotencia = await _dbConnectionFactory.CreateOpenConnection().ExecuteScalarAsync<int>(
            sql.ToString(),
            new
            {
                request.Id,
                request.Requisicao,
                request.Resultado
            });

        return idEmpotencia == 1;

    }
}
