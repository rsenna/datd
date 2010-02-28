using System.Runtime.Serialization;

namespace Dataweb.Dilab.Model.DataTransfer
{
    [DataContract]
    public class ProdutoPedido
    {
        [DataMember]
        public int CodEmpresa { get; set; }

        [DataMember]
        public int CodPedido { get; set; }

        [DataMember]
        public string CodItem { get; set; }

        [DataMember]
        public int Quantidade { get; set; }
    }
}
