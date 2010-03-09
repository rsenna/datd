using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess
{
    public interface IItemDao : IDataAccessBase<Item>
    {
        Item[] FindAll(int codFamilia, TipoItem tipo);
    }
}