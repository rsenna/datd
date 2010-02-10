using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess
{
    public interface IMaterialDao : IDataAccessBase<Material>
    {
        Material[] FindAll();
    }
}