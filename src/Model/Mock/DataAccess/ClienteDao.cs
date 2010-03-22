using Dataweb.Dilab.Model.DataTransfer;
using Base=Dataweb.Dilab.Model.DataAccess;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class ClienteDao : Base.ClienteDao
    {
        // CPF 14 CNPJ 19
        private const string SENHA = "teste123";

        public override Cliente InitDto(IReader reader, Cliente dto)
        {
            base.InitDto(reader, dto);

            // Sobreescreve a geração default para estes campos:
            dto.Cnpj = MockReader.GenerateCode(19);
            dto.Nome = MockReader.GenerateName(3);
            dto.Senha = SENHA;
            dto.NomeEmpresa = MockReader.GenerateName(5);
            dto.EmailNotificacao = MockReader.GenerateEmail();

            return dto;
        }

        public override string GetStmtFindByIdentificador()
        {
            return string.Empty;
        }

        public override string GetStmtFindByCnpj()
        {
            return string.Empty;
        }

        public override string GetStmtFindByPrimaryKey()
        {
            return string.Empty;
        }

        public override string GetStmtUpdate()
        {
            return string.Empty;
        }
    }
}