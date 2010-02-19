using System.Runtime.Serialization;

namespace Dataweb.Dilab.Model.DataTransfer
{
    /// <summary>
    /// Representa uma ordem de serviço completa para ótica, contendo os dados
    /// básicos da ordem de serviço, descrição das 2 lentes e os serviços a
    /// serem efetuados.
    /// Classe utilizada para uso exclusivo do serviço WCF.
    /// </summary>
    [DataContract]
    public class OrdemServicoOtica
    {
        [DataMember]
        public OrdemServico OrdemServico { get; set; }

        [DataMember]
        public OrdemServicoLente LenteOd { get; set; }

        [DataMember]
        public OrdemServicoLente LenteOe { get; set; }

        [DataMember]
        public ServicoOrdemServico[] Servicos { get; set; }
    }
}