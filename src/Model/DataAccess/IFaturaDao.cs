using System.Collections.Generic;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess
{
    public interface IFaturaDao : IDataAccessBase<Fatura>
    {
        IEnumerable<Fatura> FindAll(int codEmpresa);
        Fatura FindByPrimaryKey(int codEmpresa, int codFatura);
    }
}
