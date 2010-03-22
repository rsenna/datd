using Dataweb.Dilab.Model.DataTransfer;
using Base=Dataweb.Dilab.Model.DataAccess;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class FamiliaDao : Base.FamiliaDao
    {
        public override Familia InitDto(IReader reader, Familia dto)
        {
            base.InitDto(reader, dto);

            // Sobreescreve a geração default para estes campos:
            dto.Descricao = MockReader.GenerateName(4).ToUpper();

            return dto;
        }

        public override string GetStmtFindByPrimaryKey()
        {
            return string.Empty;
        }

        public override string GetStmtFindAll()
        {
            return string.Empty;
        }
    }
}