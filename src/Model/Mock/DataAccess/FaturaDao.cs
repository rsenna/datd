using Dataweb.Dilab.Model.DataTransfer;
using Base=Dataweb.Dilab.Model.DataAccess;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class FaturaDao : Base.FaturaDao
    {
        public override Fatura InitDto(IReader reader, Fatura dto)
        {
            base.InitDto(reader, dto);

            // Sobreescreve a geração default para estes campos:
            dto.Data = MockReader.GenerateDateTime(-5, 5);
            dto.Total = MockReader.GenerateDecimal(20000);

            return dto;
        }

        public override string GetStmtFindAllByCodCliente()
        {
            return string.Empty;
        }

        public override string GetStmtFindByPrimaryKey()
        {
            return string.Empty;
        }
    }
}
