namespace Dataweb.Dilab.Model
{
    public interface IDataAccessBase {}

    public interface IDataAccessBase<T> : IDataAccessBase
        where T: DataTransferBase
    {
        T FindByPrimaryKey(object pk);
        T Insert(T dto);
        T Update(T dto);
    }
}
