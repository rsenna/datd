using System.Runtime.Serialization;

namespace Dataweb.Dilab.Model.DataTransfer
{
    [DataContract]
    public class ServicoOrdemServico : DataTransferBase
    {
        [DataMember]
        public int CodEmpresa { get; set; }

        [DataMember]
        public int CodOrdemServico { get; set; }

        [DataMember]
        public string CodItem { get; set; }

        [DataMember]
        public int Quantidade { get; set; }
    }
}
