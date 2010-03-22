using Dataweb.Dilab.Model.DataTransfer;
using Base=Dataweb.Dilab.Model.DataAccess;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class ItemDao : Base.ItemDao
    {
        public override Item InitDto(IReader reader, Item dto)
        {
            base.InitDto(reader, dto);

            // Sobreescreve a geração default para estes campos:
            dto.CodItem = MockReader.GenerateCode(10);
            dto.CodBarra = MockReader.GenerateCode(10);
            dto.Descricao = MockReader.GenerateName(4).ToUpper();
            dto.Tipo = (TipoItem) MockReader.GenerateInt32(1, 2);

            return dto;
        }

        public override string GetStmtFindAllProdutoByCodFamilia()
        {
            return string.Empty;
        }

        public override string GetStmtFindAllServicoByCodFamilia()
        {
            return string.Empty;
        }
    }
}
