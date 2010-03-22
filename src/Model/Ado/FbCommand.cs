using System.Data;
using System.Transactions;

namespace Dataweb.Dilab.Model.Ado
{
    public sealed class FbCommand : ICommand
    {
        public string CommandText
        {
            get
            {
                return DbCommand.CommandText;
            }

            set
            {
                DbCommand.CommandText = value;
            }
        }

        public ISession Session { get; set; }

        private FbSession FbSession
        {
            get
            {
                return (FbSession) Session;
            }
        }

        private IDbCommand dbCommand;

        private IDbCommand DbCommand
        {
            get
            {
                if (dbCommand == null)
                {
                    dbCommand = FbSession.CreateDbCommand();
                }

                return dbCommand;
            }
        }

        public FbCommand()
        {
            if (Transaction.Current == null)
            {
                throw new TransactionException("Para executar um método DAO, é necessário estar em um TransactionScope.");
            }
        }

        public void Dispose()
        {
            if (dbCommand != null)
            {
                dbCommand.Dispose();
            }
        }

        public object this[string parameterName]
        {
            get
            {
                return DbCommand.Parameters[parameterName];
            }
        }

        public void AddParameter(string parameterName, DbType dbType, ParameterDirection direction, object value)
        {
            var parameter = DbCommand.CreateParameter();

            parameter.ParameterName = parameterName;
            parameter.DbType = dbType;
            parameter.Direction = direction;

            if ((value is bool) || (value is bool?) && (dbType == DbType.String || dbType == DbType.StringFixedLength))
            {
                value = FbReader.ToString((bool?)value);
            }

            parameter.Value = value;

            DbCommand.Parameters.Add(parameter);
        }

        public void AddParameter(string parameterName, DbType dbType, object value)
        {
            AddParameter(
                parameterName,
                dbType,
                ParameterDirection.Input,
                value
                );
        }

        public void AddParameter(string parameterName, DbType dbType, ParameterDirection direction)
        {
            AddParameter(
                parameterName,
                dbType,
                direction,
                null
                );
        }

        public void Execute()
        {
            DbCommand.ExecuteNonQuery();
        }

        public IReader ExecuteReader()
        {
            var dataReader = DbCommand.ExecuteReader();
            var result = new FbReader {Command = this, DataReader = dataReader};
            return result;
        }

        public object ExecuteScalar()
        {
            var result = DbCommand.ExecuteScalar();
            return result;
        }

        public void Prepare()
        {
            DbCommand.Prepare();
        }
    }
}