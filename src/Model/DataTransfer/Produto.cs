using System.Runtime.Serialization;

namespace Dataweb.Dilab.Model.DataTransfer
{
    [DataContract]
    public class Produto : DataTransferBase
    {
        [DataMember]
        public string CodItem { get; set; }

        [DataMember]
        public bool Obrigatorio { get; set; }

        [DataMember]
        public string Descricao { get; set; }
    }
}