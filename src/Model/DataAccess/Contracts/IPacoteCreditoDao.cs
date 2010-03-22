using System.Collections.Generic;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess.Contracts
{
    public interface IPacoteCreditoDao : IDataAccessBase<PacoteCredito>
    {
        IEnumerable<PacoteCredito> FindAll(int codCliente);
        PacoteCredito FindByPrimaryKey(int codCliente, string codPacoteCliente);
    }
}