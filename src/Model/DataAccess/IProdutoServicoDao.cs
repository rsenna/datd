using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess
{
    public interface IProdutoServicoDao : IDataAccessBase<ProdutoServico>
    {
        ProdutoServico[] FindAll(int? codFamiliaOd, int? codFamiliaOe);
    }
}