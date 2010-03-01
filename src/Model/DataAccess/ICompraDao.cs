using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess
{
    public interface ICompraDao : IDataAccessBase<Compra>
    {
        Compra[] FindAll(int codCliente);
        int GetCountFechadas(int codCliente);
        int GetCountEmProducao(int codCliente);
    }
}