using System.Collections.Generic;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess
{
    public interface IItemDao : IDataAccessBase<Item>
    {
        IEnumerable<Item> FindAll(int codFamilia, TipoItem tipo);
    }
}