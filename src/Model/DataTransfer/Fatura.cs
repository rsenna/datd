using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Dataweb.Dilab.Model.DataTransfer
{
    [DataContract]
    public class Fatura : DataTransferBase
    {
        [DataMember]
        public int CodFatura { get; set; }

        [DataMember]
        public int CodCliente { get; set; }

        [DataMember]
        public int Numero { get; set; }

        [DataMember]
        public DateTime Data { get; set; }

        [DataMember]
        public decimal Total { get; set; }

        [DataMember]
        public IEnumerable<NotaFiscal> NotasFiscais { get; set; }

        [DataMember]
        public IEnumerable<Lancamento> Lancamentos { get; set; }
    }
}
