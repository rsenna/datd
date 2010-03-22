using System;
using System.Data;

namespace Dataweb.Dilab.Model
{
    public interface ICommand : IDisposable
    {
        string CommandText { get; set; }
        ISession Session { get; set; }
        object this[string parameterName] { get; }

        void AddParameter(string parameterName, DbType dbType, ParameterDirection direction, object value);
        void AddParameter(string parameterName, DbType dbType, ParameterDirection direction);
        void AddParameter(string parameterName, DbType dbType, object value);
        void Execute();
        IReader ExecuteReader();
        object ExecuteScalar();
        void Prepare();
    }
}