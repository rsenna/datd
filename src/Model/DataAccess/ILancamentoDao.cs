using System.Collections.Generic;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess
{
    public interface ILancamentoDao : IDataAccessBase<Lancamento>
    {
        IEnumerable<Lancamento> FindAll(int codCliente);
        Lancamento FindByPrimaryKey(int codLancamento);
    }
}
