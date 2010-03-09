using System.Runtime.Serialization;

namespace Dataweb.Dilab.Model.DataTransfer
{
    [DataContract]
    public class OrdemServico : Compra
    {
        [DataMember]
        public string DescricaoArmacao { get; set; }

        [DataMember]
        public string ObservacaoArmacao { get; set; }

        [DataMember]
        public int CodMaterial { get; set; }

        [DataMember]
        public int TipoVt { get; set; }

        [DataMember]
        public decimal Ta { get; set; }

        [DataMember]
        public decimal Md { get; set; }

        [DataMember]
        public decimal Diametro { get; set; }

        [DataMember]
        public string ObservacaoLente { get; set; }

        [DataMember]
        public decimal Dp { get; set; }

        [DataMember]
        public decimal Aa { get; set; }

        [DataMember]
        public decimal Eixo { get; set; }

        [DataMember]
        public decimal Ponte { get; set; }

        [DataMember]
        public OrdemServicoLente LenteOd { get; set; }

        [DataMember]
        public OrdemServicoLente LenteOe { get; set; }

        public OrdemServico()
        {
            Tipo = TipoCompra.OrdemServico;
        }
    }
}
