using System.Runtime.Serialization;

namespace Dataweb.Dilab.Model.DataTransfer
{
    [DataContract]
    public class PacoteCredito : DataTransferBase
    {
        [DataMember]
        public string CodPacoteCredito { get; set; }

        [DataMember]
        public decimal Quantidade { get; set; }

        [DataMember]
        public string Descricao { get; set; }
    }
}
