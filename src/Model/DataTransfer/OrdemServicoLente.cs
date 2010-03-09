using System.Runtime.Serialization;

namespace Dataweb.Dilab.Model.DataTransfer
{
    public enum TipoLente
    {
        OlhoDireito = 1,
        OlhoEsquerdo = 2
    }

    [DataContract]
    public class OrdemServicoLente : DataTransferBase
    {
        [DataMember]
        public int CodEmpresa { get; set; }

        [DataMember]
        public int CodTransacao { get; set; }

        [DataMember]
        public int CodOrdemServicoLente { get; set; }

        [DataMember]
        public TipoLente TipoLente { get; set; }

        [DataMember]
        public int CodFamilia { get; set; } // NAO é gravado na base - mas é utilizado na camada business para determinar quantidade do serviço.

        [DataMember]
        public string Descricao { get; set; }

        [DataMember]
        public decimal LongeEsf { get; set; }

        [DataMember]
        public decimal LongeCil { get; set; }

        [DataMember]
        public decimal LongeEixo { get; set; }

        [DataMember]
        public decimal Adicao { get; set; }

        [DataMember]
        public decimal PertoEsf { get; set; }

        [DataMember]
        public decimal PertoCil { get; set; }

        [DataMember]
        public decimal PertoEixo { get; set; }

        [DataMember]
        public decimal Dnp { get; set; }

        [DataMember]
        public decimal Alt { get; set; }
    }
}
