using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess
{
    public interface IPedidoDao : IDataAccessBase<Pedido>
    {
        Pedido[] FindAll(int codCliente);
        ProdutoPedido[] InsertItens(ProdutoPedido[] dtos);
        Pedido Close(Pedido dto);
    }
}