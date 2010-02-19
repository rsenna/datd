using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess
{
    public interface IOrdemServicoDao : IDataAccessBase<OrdemServico>
    {
        OrdemServico[] FindAll(int codCliente);
        ServicoOrdemServico[] InsertServicos(ServicoOrdemServico[] dtos);
    }
}