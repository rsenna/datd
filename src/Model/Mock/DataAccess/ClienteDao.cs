using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class ClienteDao : DataAccessBase<Cliente>, IClienteDao
    {
        // CPF 14 CNPJ 19

        public override Cliente FetchDto()
        {
            return new Cliente {
                CodCliente = GenerateInt32(),
                Identificador = GenerateInt32(),
                Cnpj = GenerateCode(19),
                Nome = GenerateText(100),
                Senha = GenerateCode(10),
                CodEmpresa = GenerateInt32(),
                NomeEmpresa = GenerateText(100),
                EmailNotificacao = GenerateText(10) + '@' + GenerateText(10),
                ReceberNotificacao = GenerateBoolean()
            };
        }

        public Cliente FindByIdentificador(int identificador)
        {
            return FetchDto();
        }

        public Cliente FindByCnpj(string cnpj)
        {
            return FetchDto();
        }
    }
}