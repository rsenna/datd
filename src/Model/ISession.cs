using System;
using System.Data;

namespace Dataweb.Dilab.Model
{
    public interface ISession : IDisposable
    {
        IDbConnection Connection { get; }

        /* TODO: passar todos os métodos do helper para cá -> isso é a base para permitir remover praticamente toda a camada de mock!
        IDbCommand CreateCommand(string stmt);
        IDataParameter AddParameter(IDbCommand command, string parameterName, DbType dbType, ParameterDirection direction, object value);
         */
    }
}
