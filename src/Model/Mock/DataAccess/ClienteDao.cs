using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class ClienteDao : DataAccessBase<Cliente>, IClienteDao
    {
        // CPF 14 CNPJ 19
        private const string SENHA = "teste123";

        public override Cliente InitDto(Cliente dto)
        {
            dto.CodCliente = GenerateInt32();
            dto.Identificador = GenerateInt32();
            dto.Cnpj = GenerateCode(19);
            dto.Nome = GenerateName(3);
            dto.Senha = SENHA;
            dto.CodEmpresa = GenerateInt32();
            dto.NomeEmpresa = GenerateName(5);
            dto.EmailNotificacao = GenerateEmail();
            dto.ReceberNotificacao = GenerateBoolean();

            return dto;
        }

        public Cliente FindByIdentificador(int identificador)
        {
            return InitDto(new Cliente());
        }

        public Cliente FindByCnpj(string cnpj)
        {
            return InitDto(new Cliente());
        }
    }
}