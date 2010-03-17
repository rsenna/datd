using System.Runtime.Serialization;

namespace Dataweb.Dilab.Model.DataTransfer
{
    [DataContract]
    public class CompraDetalhe : DataTransferBase
    {
        [DataMember]
        public int CodEmpresa { get; set; }

        [DataMember]
        public int CodCompra { get; set; }

        [DataMember]
        public string Descricao { get; set; }
    }
}