using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class ClienteDao : DataAccessBase<Cliente>, IClienteDao
    {
        // CPF 14 CNPJ 19
        private const string SENHA = "teste123";

        public override Cliente FetchDto()
        {
            return new Cliente {
                CodCliente = GenerateInt32(),
                Identificador = GenerateInt32(),
                Cnpj = GenerateCode(19),
                Nome = GenerateName(3),
                Senha = SENHA,
                CodEmpresa = GenerateInt32(),
                NomeEmpresa = GenerateName(5),
                EmailNotificacao = GenerateEmail(),
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