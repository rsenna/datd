using System;
using System.Runtime.Serialization;

namespace Dataweb.Dilab.Model.DataTransfer
{
    public class NotaFiscal : DataTransferBase
    {
        [DataMember]
        public int CodNotaFiscal { get; set; }

        [DataMember]
        public int CodCliente { get; set; }

        [DataMember]
        public int CodFatura { get; set; }

        [DataMember]
        public int Numero { get; set; }

        [DataMember]
        public DateTime Data { get; set; }

        [DataMember]
        public decimal Total { get; set; }

        [DataMember]
        public Transacao[] Transacoes { get; set; }
    }
}
