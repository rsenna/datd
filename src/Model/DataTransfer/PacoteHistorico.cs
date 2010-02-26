using System;
using System.Runtime.Serialization;

namespace Dataweb.Dilab.Model.DataTransfer
{
    public enum TipoPacoteHistorico
    {
        Compra = 1,
        Uso = 2
    }

    [DataContract]
    public class PacoteHistorico : DataTransferBase
    {
        [DataMember]
        public DateTime Data { get; set; }

        [DataMember]
        public int? NumeroOS { get; set; }

        [DataMember]
        public decimal Quantidade { get; set; }

        [DataMember]
        public decimal? Valor { get; set; }

        [DataMember]
        public TipoPacoteHistorico Tipo { get; set; }
    }
}
