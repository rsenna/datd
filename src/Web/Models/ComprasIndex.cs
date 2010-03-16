using System.Collections.Generic;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Web.Models
{
    public class ComprasIndex
    {
        public IEnumerable<Transacao> OrdensServico { get; set; }
        public IEnumerable<Transacao> Pedidos { get; set; }
    }
}
