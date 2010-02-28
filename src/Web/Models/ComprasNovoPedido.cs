using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Web.Models
{
    public class ComprasNovoPedido
    {
        // Apresentacao:
        public Familia[] Familias { get; set; }

        // Submit:
        public string[] ProdutosPedido { get; set; }
    }
}
