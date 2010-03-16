using System.Runtime.Serialization;

namespace Dataweb.Dilab.Model.DataTransfer
{
    [DataContract]
    public class Pedido : Transacao
    {
        public Pedido()
        {
            Tipo = TipoTransacao.Pedido;
        }
    }
}
