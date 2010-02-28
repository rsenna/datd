using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess
{
    public interface IServicoDao : IDataAccessBase<Servico>
    {
        Servico[] FindAll(int codFamilia);
    }
}