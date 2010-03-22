using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Transactions;

namespace Dataweb.Dilab.Model.Ado
{
    public sealed class FbSession : ISession
    {
        public const string CONNECTION_STRING_NAME = "DilabDatabase";

        private IDbConnection connection;

        public FbSession()
        {
            if (Transaction.Current == null)
            {
                throw new TransactionException("Para criar um DAO, é necessário estar em um TransactionScope.");
            }
        }

        private static ConnectionStringSettings GetConnectionString()
        {
            var connectionString = ConfigurationManager.ConnectionStrings[CONNECTION_STRING_NAME];

            if (connectionString == null)
            {
                throw new ConfigurationErrorsException(String.Format("Connection string '{0}' is undefined.", CONNECTION_STRING_NAME));
            }

            return connectionString;
        }

        private IDbConnection GetConnection()
        {
            if (connection == null)
            {
                var connectionString = GetConnectionString();
                var factory = DbProviderFactories.GetFactory(connectionString.ProviderName);

                connection = factory.CreateConnection();

                if (connection == null)
                {
                    throw new Exception("Could not obtain a connection from DB factory.");
                }

                connection.ConnectionString = connectionString.ConnectionString;
                connection.Open();
            }

            return connection;
        }

        public ICommand CreateCommand()
        {
            return new FbCommand {Session = this};
        }

        internal IDbCommand CreateDbCommand()
        {
            return GetConnection().CreateCommand();
        }

        /// <summary>
        /// Fecha a conexão quando o objeto for destruído.
        /// </summary>
        /// <remarks>
        /// Como a classe é sealed, implementação do Dispose pode ser simples.
        /// Ver <see cref="http://www.codeproject.com/KB/cs/idisposable.aspx"/>.
        /// </remarks>
        public void Dispose()
        {
            if (connection == null)
            {
                return;
            }

            if (connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }

            connection.Dispose();
        }
    }
}
