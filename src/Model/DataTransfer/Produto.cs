using System.Runtime.Serialization;

namespace Dataweb.Dilab.Model.DataTransfer
{
    [DataContract]
    public class Produto : DataTransferBase
    {
        [DataMember]
        public string CodItem { get; set; }

        [DataMember]
        public string CodBarra { get; set; }

        [DataMember]
        public string Descricao { get; set; }
    }
}