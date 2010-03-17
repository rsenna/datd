using System.Collections.Generic;
using System.ServiceModel;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Service
{
    [ServiceContract]
    public interface IProdutoService : IService
    {
        [OperationContract]
        IEnumerable<Transacao> FindAllTransacaoByLogin(string login);

        [OperationContract]
        IEnumerable<Transacao> FindAllTransacaoByLoginAndReferencia(string login, string referencia);

        [OperationContract]
        IEnumerable<Familia> FindAllFamilia();

        [OperationContract]
        IEnumerable<Material> FindAllMaterial();

        [OperationContract]
        int GetCountFechadas(int codCliente);

        [OperationContract]
        int GetCountEmProducao(int codCliente);

        [OperationContract]
        IEnumerable<Item> FindAllServico(int codFamilia);

        [OperationContract]
        IEnumerable<Item> FindAllProduto(int codFamilia);

        [OperationContract]
        Transacao GetTransacao(int codEmpresa, int codTransacao);

        [OperationContract]
        OrdemServico GetOrdemServico(int codEmpresa, int codTransacao);

        [OperationContract]
        Pedido GetPedido(int codEmpresa, int codTransacao);

        [OperationContract]
        OrdemServico InsertOrdemServico(OrdemServico dto);

        [OperationContract]
        Pedido InsertPedido(Pedido dto);

        [OperationContract]
        IEnumerable<Fatura> FindAllFatura(string login);

        [OperationContract]
        IEnumerable<Lancamento> FindAllLancamento(string login);

        [OperationContract]
        IEnumerable<NotaFiscal> FindAllNotaFiscal(string login);

        [OperationContract]
        Fatura GetFatura(int codFatura);

        [OperationContract]
        NotaFiscal GetNotaFiscal(int codNotaFiscal);

        [OperationContract]
        string GetXmlNotaFiscalEletronica(int codNotaFiscal);
    }
}