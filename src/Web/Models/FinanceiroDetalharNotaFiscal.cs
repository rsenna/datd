using System.Collections.Generic;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Web.Models
{
    public class FinanceiroDetalharNotaFiscal
    {
        public NotaFiscal NotaFiscal { get; set; }
        public IEnumerable<Transacao> OrdensServico { get; set; }
        public IEnumerable<Transacao> Pedidos { get; set; }
    }
}
