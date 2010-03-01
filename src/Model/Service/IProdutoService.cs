using System.ServiceModel;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Service
{
    [ServiceContract]
    public interface IProdutoService : IService
    {
        [OperationContract]
        Compra[] FindAllCompraByCodCliente(int codCliente);

        [OperationContract]
        Compra[] FindAllCompraByCodClienteAndReferencia(int codCliente, string referencia);

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
        Servico[] FindAllServico(int codFamilia);

        [OperationContract]
        Produto[] FindAllProduto(int codFamilia);

        [OperationContract]
        OrdemServicoOtica InsertOrdemServico(OrdemServicoOtica dto);

        [OperationContract]
        Pedido InsertPedido(Pedido dto);
    }
}