using System.Collections.Generic;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Web.Models
{
    public class ComprasIndex
    {
        public IEnumerable<Compra> OrdensServico { get; set; }
        public IEnumerable<Compra> Pedidos { get; set; }
    }
}
