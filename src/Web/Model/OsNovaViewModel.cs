using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Web.Model
{
    public class OsNovaViewModel
    {
        // Saída: ///////////

        public Familia[] Familias { get; set; }
        public Material[] Materiais { get; set; }

        // Entrada: /////////

        public int NumeroOs { get; set; }

        public int FamiliaOd { get; set; }
        public decimal EsfLongeOd { get; set; }
        public decimal CilLongeOd { get; set; }
        public decimal EixoLongeOd { get; set; }
        public decimal AdicaoOd { get; set; }
        public decimal EsfPertoOd { get; set; }
        public decimal CilPertoOd { get; set; }
        public decimal EixoPertoOd { get; set; }
        public decimal DnpOd { get; set; }
        public decimal AltOd { get; set; }
        public string DescricaoLenteOd { get; set; }

        public int FamiliaOe { get; set; }
        public decimal EsfLongeOe { get; set; }
        public decimal CilLongeOe { get; set; }
        public decimal EixoLongeOe { get; set; }
        public decimal AdicaoOe { get; set; }
        public decimal EsfPertoOe { get; set; }
        public decimal CilPertoOe { get; set; }
        public decimal EixoPertoOe { get; set; }
        public decimal DnpOe { get; set; }
        public decimal AltOe { get; set; }
        public string DescricaoLenteOe { get; set; }

        public decimal Dp { get; set; }

        public decimal Ta { get; set; }
        public decimal Aa { get; set; }
        public decimal Md { get; set; }
        public decimal Eixo { get; set; }
        public decimal Ponte { get; set; }
        public decimal Diametro { get; set; }

        public string Armacao { get; set; }
        public string ObservacaoArmacao { get; set; }
        public int MaterialArmacao { get; set; }
        public string ObservacaoGeral { get; set; }
    }
}
