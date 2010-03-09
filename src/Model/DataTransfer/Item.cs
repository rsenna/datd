using System.Runtime.Serialization;

namespace Dataweb.Dilab.Model.DataTransfer
{
    public enum TipoItem
    {
        Produto = 1,
        Servico = 2
    }

    [DataContract]
    public class Item : DataTransferBase
    {
        [DataMember]
        public string CodItem { get; set; }

        [DataMember]
        public string CodBarra { get; set; }

        [DataMember]
        public bool Obrigatorio { get; set; }

        [DataMember]
        public string Descricao { get; set; }

        [DataMember]
        public bool ServicoInterno { get; set; }

        [DataMember]
        public TipoItem Tipo { get; set; }
    }
}
