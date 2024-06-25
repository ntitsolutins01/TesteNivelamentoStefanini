using System.Data;
using System.Text;
using Dapper;
using QuestaoCinco.Application.Common.Interfaces;
using QuestaoCinco.Domain.Entities;

namespace QuestaoCinco.Application.IdEmpotencias.Commands.CreateIdEmpotencia;

public record CreateIdEmpotenciaCommand : IRequest<bool>
{
    public string? Requisicao { get; init; }
    public string? Resultado { get; init; }
}

public class CreateIdEmpotenciaCommandHandler : IRequestHandler<CreateIdEmpotenciaCommand, bool>
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public CreateIdEmpotenciaCommandHandler(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<bool> Handle(CreateIdEmpotenciaCommand request, CancellationToken cancellationToken)
    {
        var sql = new StringBuilder(@"INSERT INTO IdEmpotencia (Requisicao, Resultado, Created, LastModified) VALUES (@Requisicao, @Resultado, getdate(), getdate())");

        var idEmpotencia = await _dbConnectionFactory.CreateOpenConnection().ExecuteScalarAsync<int>(
            sql.ToString(),
            new
            {
                request.Requisicao,
                request.Resultado
            });

        return idEmpotencia == 1;
    }
}


