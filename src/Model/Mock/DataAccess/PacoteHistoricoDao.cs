using Dataweb.Dilab.Model.DataTransfer;
using Base=Dataweb.Dilab.Model.DataAccess;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class PacoteHistoricoDao : Base.PacoteHistoricoDao
    {
        public override PacoteHistorico InitDto(IReader reader, PacoteHistorico dto)
        {
            base.InitDto(reader, dto);

            // Sobreescreve a geração default para estes campos:
            dto.Data = MockReader.GenerateDateTime(-10);
            dto.Quantidade = MockReader.GenerateInt32(100);
            dto.Valor = MockReader.GenerateDecimal(10000);

            return dto;
        }

        public override string GetStmtFindByCodClienteAndCodPacote()
        {
            return string.Empty;
        }
    }
}