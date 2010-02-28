using System;
using System.Runtime.Serialization;

namespace Dataweb.Dilab.Model.DataTransfer
{
    public enum StatusPedido
    {
        Aberto,
        Fechado,
        Cancelado
    }

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
        public DateTime Emissao { get; set; }

        [DataMember]
        public DateTime Encerramento { get; set; }

        [DataMember]
        public DateTime Expedicao { get; set; }

        [DataMember]
        public bool EnviadoEmail { get; set; }

        [DataMember]
        public string Observacao { get; set; }

        [DataMember]
        public StatusPedido Status { get; set; }

        [DataMember]
        public ProdutoPedido[] Produtos { get; set; }
    }
}
