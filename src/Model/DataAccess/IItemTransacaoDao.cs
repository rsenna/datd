using System.Collections.Generic;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess
{
    public interface IItemTransacaoDao : IDataAccessBase<ItemTransacao>
    {
        IEnumerable<ItemTransacao> FindAll(int codEmpresa, int codTransacao);
        IEnumerable<ItemTransacao> Insert(IEnumerable<ItemTransacao> dtos);
    }
}
