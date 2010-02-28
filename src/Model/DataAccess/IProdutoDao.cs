using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess
{
    public interface IProdutoDao : IDataAccessBase<Produto>
    {
        Produto[] FindAll(int codFamilia);
    }
}