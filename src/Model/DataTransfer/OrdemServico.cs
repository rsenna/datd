using System.Runtime.Serialization;

namespace Dataweb.Dilab.Model.DataTransfer
{
    /// <summary>
    /// Representa uma ordem de serviço básica (i.e. não agregada as
    /// informações de lentes e serviços). O ideal seria que ela não possuisse
    /// sequer informações específicas de ótica, mas isso acontece por limitação
    /// das próprias procs expostas.
    /// </summary>
    [DataContract]
    public class OrdemServico : DataTransferBase
    {
        [DataMember]
        public int CodOrdemServico { get; set; }

        [DataMember]
        public int Numero { get; set; }
        
        [DataMember]
        public int CodEmpresa { get; set; }

        [DataMember]
        public int CodCliente { get; set; }

        [DataMember]
        public string Observacao { get; set; }

        [DataMember]
        public string Referencia { get; set; }

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
    }
}
