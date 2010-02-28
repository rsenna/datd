using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess
{
    public interface IPedidoDao : IDataAccessBase<Pedido>
    {
        Pedido[] FindAll(int codCliente);
        ProdutoPedido[] InsertProdutos(ProdutoPedido[] dtos);
    }
}