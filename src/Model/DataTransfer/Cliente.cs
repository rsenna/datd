using System.Runtime.Serialization;

namespace Dataweb.Dilab.Model.DataTransfer
{
    [DataContract]
    public class Cliente : DataTransferBase
    {
        [DataMember]
        public int CodCliente { get; set; } // COD_CLIENTE

        [DataMember]
        public int Identificador { get; set; } // IDENTIFICADOR

        [DataMember]
        public string Cnpj { get; set; } // CNPJ

        [DataMember]
        public string Nome { get; set; } // NOME

        [DataMember]
        public int CodEmpresa { get; set; } // COD_EMPRESA

        [DataMember]
        public string NomeEmpresa { get; set; } // NOMEEMPRESA

        [DataMember]
        public string EmailNotificacao { get; set; } // EMAILNOTIFICACAO

        [DataMember]
        public string Senha { get; set; }

        [DataMember]
        public bool ReceberNotificacao { get; set; } // RECEBERNOTIFICACAO
    }
}