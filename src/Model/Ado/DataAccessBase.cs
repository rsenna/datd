using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Transactions;

namespace Dataweb.Dilab.Model.Ado
{
    public abstract class DataAccessBase<T> : IDataAccessBase<T>
        where T: DataTransferBase, new()
    {
        protected DataAccessBase()
        {
            if (Transaction.Current == null)
            {
                throw new TransactionException("Para criar um DAO, é necessário estar em um TransactionScope.");
            }
        }

        public QueryDepth Depth { get; set; }
        public ISession Session { get; set; }

        public virtual T InitDto(DbCommand c, T dto)
        {
            using (var reader = c.ExecuteReader())
            {
                if (reader.Read())
                {
                    return InitDto(reader, dto);
                }
            }

            return dto;
        }

        public abstract T InitDto(IDataRecord record, T dto);

        public virtual IEnumerable<T> InitDtos(DbCommand c, IList<T> dtos)
        {
            using (var reader = c.ExecuteReader())
            {
                while (reader.Read())
                {
                    var item = InitDto(reader, new T());
                    dtos.Add(item);
                }
            }

            return dtos;
        }

        public abstract IEnumerable<T> FindAll();
        public abstract T FindByPrimaryKey(object pk);
        public abstract T Insert(T dto);
        public abstract T Update(T dto);

        protected IEnumerable<T> FindAll(string sqlStmt)
        {
            IEnumerable<T> result = null;

            Helper.UsingCommand(Session.Connection, c =>
            {
                c.CommandText = sqlStmt;
                result = InitDtos(c, new List<T>());
            });

            return result;
        }

        protected QueryDepth GetDetailDepth()
        {
            switch (Depth)
            {
                case QueryDepth.None:
                case QueryDepth.FirstLevel:
                    return QueryDepth.None;

                case QueryDepth.Complete:
                    return QueryDepth.Complete;

                default:
                    return Depth - 1;
            }
        }
    }
}