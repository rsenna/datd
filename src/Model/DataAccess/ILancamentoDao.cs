using System.Collections.Generic;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess
{
    public interface ILancamentoDao : IDataAccessBase<Lancamento>
    {
        IEnumerable<Lancamento> FindAll(int codEmpresa);
        Lancamento FindByPrimaryKey(int codEmpresa, int codLancamento);
    }
}
