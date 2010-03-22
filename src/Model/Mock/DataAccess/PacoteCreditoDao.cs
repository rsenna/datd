using Dataweb.Dilab.Model.DataTransfer;
using Base=Dataweb.Dilab.Model.DataAccess;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class PacoteCreditoDao : Base.PacoteCreditoDao
    {
        public override PacoteCredito InitDto(IReader reader, PacoteCredito dto)
        {
            base.InitDto(reader, dto);

            // Sobreescreve a geração default para estes campos:
            dto.CodPacoteCredito = MockReader.GenerateCode(10);
            dto.Descricao = MockReader.GenerateName(2);
            dto.Quantidade = MockReader.GenerateInt32(100);

            return dto;
        }

        public override string GetStmtFindByCodCliente()
        {
            return string.Empty;
        }

        public override string GetStmtFindByPrimaryKey()
        {
            return string.Empty;
        }
    }
}