using System.Data;
using System.Data.Common;

namespace Dataweb.Dilab.Model.Ado
{
    public sealed class Session : ISession
    {
        private IDbConnection connection;

        public IDbConnection Connection
        {
            get
            {
                if (connection == null)
                {
                    connection = Helper.OpenConnection();
                }

                return connection;
            }
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
            if (connection != null && connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }
    }
}
