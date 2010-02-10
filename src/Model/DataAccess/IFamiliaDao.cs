using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess
{
    public interface IFamiliaDao : IDataAccessBase<Familia>
    {
        Familia[] FindAll();
    }
}