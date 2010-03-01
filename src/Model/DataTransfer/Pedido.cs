using System.Runtime.Serialization;

namespace Dataweb.Dilab.Model.DataTransfer
{
    [DataContract]
    public class Pedido : DataTransferBase
    {
        [DataMember]
        public int CodPedido { get; set; }

        [DataMember]
        public int Numero { get; set; }

        [DataMember]
        public int CodEmpresa { get; set; }

        [DataMember]
        public int CodCliente { get; set; }

        [DataMember]
        public string Observacao { get; set; }

        [DataMember]
        public ProdutoPedido[] Produtos { get; set; }
    }
}
