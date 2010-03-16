using System.Collections.Generic;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess
{
    public interface INotaFiscalDao : IDataAccessBase<NotaFiscal>
    {
        IEnumerable<NotaFiscal> FindAll(int codEmpresa);
        NotaFiscal FindByPrimaryKey(int codEmpresa, int codNotaFiscal);
    }
}
