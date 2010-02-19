using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess
{
    public interface IOrdemServicoLenteDao : IDataAccessBase<OrdemServicoLente>
    {
        OrdemServicoLente[] FindAll(int codOrdemServico);
    }
}