using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Web.Models
{
    public class ComprasDetalhar
    {
        public Transacao Transacao { get; set; }

        public OrdemServico OrdemServico
        {
            get { return Transacao as OrdemServico; }
        }

        public Pedido Pedido
        {
            get { return Transacao as Pedido; }
        }

        public bool IsOrdemServico
        {
            get { return Transacao is OrdemServico; }
        }

        public bool IsPedido
        {
            get { return Transacao is Pedido; }
        }
    }
}
