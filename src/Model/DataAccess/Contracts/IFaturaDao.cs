using System.Collections.Generic;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess.Contracts
{
    public interface IFaturaDao : IDataAccessBase<Fatura>
    {
        IEnumerable<Fatura> FindAll(int codCliente);
        Fatura FindByPrimaryKey(int codFatura);
    }
}