using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess
{
    public interface IPacoteHistoricoDao : IDataAccessBase<PacoteHistorico>
    {
        PacoteHistorico[] FindAll(int codCliente, string codPacoteCliente);
    }
}