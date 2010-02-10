using System.Runtime.Serialization;

namespace Dataweb.Dilab.Model.DataTransfer
{
    [DataContract]
    public class Material : DataTransferBase
    {
        [DataMember]
        public int CodMaterial { get; set; }

        [DataMember]
        public string Descricao { get; set; }
    }
}
