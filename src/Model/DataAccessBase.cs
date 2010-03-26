using System.Collections.Generic;

namespace Dataweb.Dilab.Model
{
    public abstract class DataAccessBase<T> : IDataAccessBase<T>
        where T: DataTransferBase, new()
    {
        public QueryDepth Depth { get; set; }
        public ISession Session { get; set; }

        public virtual T InitDto(ICommand c, T dto)
        {
            using (var reader = c.ExecuteReader())
            {
                return reader.ReadRecord()? InitDto(reader, dto) : null;
            }
        }

        public abstract T InitDto(IReader reader, T dto);

        public virtual IEnumerable<T> InitDtos(ICommand c, IList<T> dtos)
        {
            using (var reader = c.ExecuteReader())
            {
                while (reader.ReadRecord())
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
            IEnumerable<T> result;

            using (var c = Session.CreateCommand())
            {
                c.CommandText = sqlStmt;
                result = InitDtos(c, new List<T>());
            }

            return result;
        }

        public QueryDepth GetDetailDepth()
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