using System.ServiceModel;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Wcf.Contract
{
    [ServiceContract]
    public interface IOrdemServicoService : IService
    {
        [OperationContract]
        OrdemServico[] FindAllByCodCliente(int codCliente);

        [OperationContract]
        OrdemServico[] FindAllByCodClienteAndReferencia(int codCliente, string referencia);

        [OperationContract]
        OrdemServico[] FindAllByLogin(string login);

        [OperationContract]
        OrdemServico[] FindAllByLoginAndReferencia(string login, string referencia);

        [OperationContract]
        Familia[] FindAllFamilia();

        [OperationContract]
        Material[] FindAllMaterial();

        [OperationContract]
        int GetCountFechadas(int codCliente);

        [OperationContract]
        int GetCountEmProducao(int codCliente);

        [OperationContract]
        ProdutoServico[] FindAllProdutoServico(int? codFamiliaOd, int? codFamiliaOe);
    }
}