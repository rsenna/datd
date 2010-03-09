using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess
{
    public enum ProfundidadeConsultaTransacao
    {
        Transacao,
        Especializacao,
        Item,
        Completa
    }

    public interface ICompraDao : IDataAccessBase<Compra>
    {
        Compra[] FindAll(int codCliente, ProfundidadeConsultaTransacao profundidade);
        int GetCountFechadas(int codCliente);
        int GetCountEmProducao(int codCliente);
        Compra Close(Compra dto);
    }
}