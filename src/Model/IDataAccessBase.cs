using System.Collections.Generic;

namespace Dataweb.Dilab.Model
{
    public enum QueryDepth
    {
        None = 0,
        FirstLevel = 1,
        SecondLevel = 2,

        Complete = 999
    }

    public interface IDataAccessBase
    {
        QueryDepth Depth { get; set; }
        ISession Session { get; set; }
    }

    public interface IDataAccessBase<T> : IDataAccessBase
        where T: DataTransferBase
    {
        IEnumerable<T> FindAll();
        T FindByPrimaryKey(object pk);
        T Insert(T dto);
        T Update(T dto);
    }
}
