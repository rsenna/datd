using System.ServiceModel;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Service
{
    [ServiceContract]
    public interface IProdutoService : IService
    {
        [OperationContract]
        Compra[] FindAllCompraByLogin(string login);

        [OperationContract]
        Compra[] FindAllCompraByLoginAndReferencia(string login, string referencia);

        [OperationContract]
        Familia[] FindAllFamilia();

        [OperationContract]
        Material[] FindAllMaterial();

        [OperationContract]
        int GetCountFechadas(int codCliente);

        [OperationContract]
        int GetCountEmProducao(int codCliente);

        [OperationContract]
        Item[] FindAllServico(int codFamilia);

        [OperationContract]
        Item[] FindAllProduto(int codFamilia);

        [OperationContract]
        OrdemServico InsertOrdemServico(OrdemServico dto);

        [OperationContract]
        Pedido InsertPedido(Pedido dto);
    }
}