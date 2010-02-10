using System.Runtime.Serialization;

namespace Dataweb.Dilab.Model.DataTransfer
{
    [DataContract]
    public class Familia : DataTransferBase
    {
        [DataMember]
        public int CodFamilia { get; set; }

        [DataMember]
        public string Descricao { get; set; }
    }
}
