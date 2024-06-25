using System.Data;

namespace QuestaoCinco.Application.Common.Interfaces;

public interface IDbConnectionFactory
{
    IDbConnection CreateOpenConnection();
}
