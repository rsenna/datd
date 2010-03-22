using System;
using System.Runtime.Serialization;

namespace Dataweb.Dilab.Model.DataTransfer
{
    public class Lancamento : DataTransferBase
    {
        [DataMember]
        public int CodLancamento { get; set; }

        [DataMember]
        public int CodCliente { get; set; }

        [DataMember]
        public int? CodFatura { get; set; }

        [DataMember]
        public string Documento { get; set; }

        [DataMember]
        public DateTime Vencimento { get; set; }

        [DataMember]
        public DateTime? Pagamento { get; set; }

        [DataMember]
        public decimal Total { get; set; }
    }
}
