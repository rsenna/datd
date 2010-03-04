using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Transactions;

namespace Dataweb.Dilab.Model.Ado
{
    public abstract class DataAccessBase<T> : IDataAccessBase<T>
        where T: DataTransferBase
    {
        protected DataAccessBase()
        {
            if (Transaction.Current == null)
            {
                throw new TransactionException("Para criar um DAO, é necessário estar em um TransactionScope.");
            }
        }

        public ISession Session { get; set; }

        public virtual T FetchDto(DbCommand c)
        {
            T result = null;

            using (var reader = c.ExecuteReader())
            {
                if (reader.Read())
                {
                    result = FetchDto(reader);
                }
            }

            return result;
        }

        public abstract T FetchDto(IDataRecord record);

        public virtual T[] FetchDtos(DbCommand c)
        {
            var result = new List<T>();

            using (var reader = c.ExecuteReader())
            {
                while (reader.Read())
                {
                    var item = FetchDto(reader);
                    result.Add(item);
                }
            }

            return result.ToArray();
        }

        public abstract T[] FindAll();
        public abstract T FindByPrimaryKey(object pk);
        public abstract T Insert(T dto);
        public abstract T Update(T dto);

        protected T[] FindAll(string sqlStmt)
        {
            T[] result = null;

            Helper.UsingCommand(Session.Connection, c =>
            {
                c.CommandText = sqlStmt;
                result = FetchDtos(c);
            });

            return result;
        }
    }
}