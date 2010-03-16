using System.Runtime.Serialization;

namespace Dataweb.Dilab.Model.DataTransfer
{
    [DataContract]
    public class ItemTransacao : DataTransferBase
    {
        [DataMember]
        public int CodEmpresa { get; set; }

        [DataMember]
        public int CodTransacao { get; set; }

        [DataMember]
        public string CodItem { get; set; }

        [DataMember]
        public string Descricao { get; set; } // Redundancia sobre Item.Descricao; utilizado apenas na LEITURA da base.

        [DataMember]
        public int Quantidade { get; set; }
    }
}
