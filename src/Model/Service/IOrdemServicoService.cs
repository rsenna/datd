using System.ServiceModel;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Service
{
    [ServiceContract]
    public interface IOrdemServicoService : IService
    {
        [OperationContract]
        OrdemServicoQuery[] FindAllByCodCliente(int codCliente);

        [OperationContract]
        OrdemServicoQuery[] FindAllByCodClienteAndReferencia(int codCliente, string referencia);

        [OperationContract]
        OrdemServicoQuery[] FindAllByLogin(string login);

        [OperationContract]
        OrdemServicoQuery[] FindAllByLoginAndReferencia(string login, string referencia);

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
        void InsertOrdemServico(OrdemServicoOtica dto);
    }
}