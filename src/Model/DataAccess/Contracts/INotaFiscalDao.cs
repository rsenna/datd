using System.Collections.Generic;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess.Contracts
{
    public interface INotaFiscalDao : IDataAccessBase<NotaFiscal>
    {
        IEnumerable<NotaFiscal> FindAll(int codCliente);
        IEnumerable<NotaFiscal> FindAll(int codCliente, int codFatura);
        NotaFiscal FindByPrimaryKey(int codNotaFiscal);
    }
}