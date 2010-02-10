namespace Dataweb.Dilab.Model
{
    public interface IDataAccessBase {}

    public interface IDataAccessBase<T> : IDataAccessBase
        where T: DataTransferBase
    {
        T FindByPrimaryKey(object pk);
        void Update(T dto);
    }
}
