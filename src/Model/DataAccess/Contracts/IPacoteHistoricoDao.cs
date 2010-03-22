using System.Collections.Generic;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess.Contracts
{
    public interface IPacoteHistoricoDao : IDataAccessBase<PacoteHistorico>
    {
        IEnumerable<PacoteHistorico> FindAll(int codCliente, string codPacoteCliente);
    }
}