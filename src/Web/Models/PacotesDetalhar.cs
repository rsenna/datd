using System.Collections.Generic;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Web.Models
{
    public class PacotesDetalhar
    {
        public string Descricao { get; set; }

        public IEnumerable<PacoteHistorico> Compra { get; set; }
        public IEnumerable<PacoteHistorico> Uso { get; set; }

        public decimal TotalCompras { get; set; }
        public decimal TotalUsos { get; set; }
    }
}