using System.Runtime.Serialization;

namespace Dataweb.Dilab.Model.DataTransfer
{
    [DataContract]
    public class Pedido : Compra
    {
        public Pedido()
        {
            Tipo = TipoCompra.Pedido;
        }
    }
}
