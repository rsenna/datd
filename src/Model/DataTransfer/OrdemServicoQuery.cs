using System;
using System.Runtime.Serialization;

namespace Dataweb.Dilab.Model.DataTransfer
{
    public enum TipoEtapa
    {
        Inicial = 0,
        EmProducao = 1,
        Expedicao = 2,
        Despachada = 3,
        Finalizada = 4
    }

    [DataContract]
    public class OrdemServicoQuery : DataTransferBase
    {
        [DataMember]
        public int CodEmpresa { get; set; } // RCODEMPRESA

        [DataMember]
        public int CodTransacao { get; set; } // RCODTRANSACAO

        [DataMember]
        public int NumeroOrdemServico { get; set; } // RNUMEROORDEMSERVICO

        [DataMember]
        public string Referencia { get; set; } // RREFERENCIA

        [DataMember]
        public DateTime Emissao { get; set; } // RDATAHORAEMISSAO

        [DataMember]
        public DateTime? Previsao { get; set; } // RDATAHORAPREVISAO

        [DataMember]
        public DateTime? Expedicao { get; set; } // RDATAHORAEXPEDICAO

        [DataMember]
        public TipoEtapa Etapa { get; set; } // RCODETAPA

        [DataMember]
        public string AvisoMensagem { get; set; } // RAVISOMENSAGEM
    }
}