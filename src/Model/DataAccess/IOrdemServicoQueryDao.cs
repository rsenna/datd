using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess
{
    public interface IOrdemServicoQueryDao : IDataAccessBase<OrdemServicoQuery>
    {
        OrdemServicoQuery[] FindAll(int codCliente);
        int GetCountFechadas(int codCliente);
        int GetCountEmProducao(int codCliente);
    }
}