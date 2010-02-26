using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess
{
    public interface IPacoteCreditoDao : IDataAccessBase<PacoteCredito>
    {
        PacoteCredito[] FindAll(int codCliente);
        PacoteCredito FindByPrimaryKey(int codCliente, string codPacoteCliente);
    }
}