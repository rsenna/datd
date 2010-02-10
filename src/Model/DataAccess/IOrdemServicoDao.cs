using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess
{
    public interface IOrdemServicoDao : IDataAccessBase<OrdemServico>
    {
        OrdemServico[] FindAll(int codCliente);
        int GetCountFechadas(int codCliente);
        int GetCountEmProducao(int codCliente);
    }
}