using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess
{
    public interface IOrdemServicoDao : IDataAccessBase<OrdemServico>
    {
        OrdemServico[] FindAll(int codCliente);
        ServicoOrdemServico[] InsertItens(ServicoOrdemServico[] dtos);
        OrdemServico Close(OrdemServico dto);
    }
}