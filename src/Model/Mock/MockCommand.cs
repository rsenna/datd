using System.Collections.Generic;
using System.Data;

namespace Dataweb.Dilab.Model.Mock
{
    public class MockCommand : ICommand
    {
        private readonly Dictionary<string, object> parameters;

        public MockCommand()
        {
            parameters = new Dictionary<string, object>();
        }

        public void Dispose() {}

        public string CommandText { get; set; }
        public ISession Session { get; set; }

        public object this[string parameterName]
        {
            get
            {
                return parameters[parameterName];
            }
        }

        public void AddParameter(string parameterName, DbType dbType, ParameterDirection direction, object value)
        {
            parameters.Add(parameterName, value);
        }

        public void AddParameter(string parameterName, DbType dbType, ParameterDirection direction)
        {
            parameters.Add(parameterName, null);
        }

        public void AddParameter(string parameterName, DbType dbType, object value)
        {
            parameters.Add(parameterName, value);
        }

        public void Execute() {}

        public IReader ExecuteReader()
        {
            return new MockReader {Command = this};
        }

        public object ExecuteScalar()
        {
            return 0;
        }

        public void Prepare() {}
    }
}
