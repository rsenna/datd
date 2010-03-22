using Dataweb.Dilab.Model.DataTransfer;
using Base=Dataweb.Dilab.Model.DataAccess;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class ItemTransacaoDao : Base.ItemTransacaoDao
    {
        public override ItemTransacao InitDto(IReader reader, ItemTransacao dto)
        {
            base.InitDto(reader, dto);

            // Sobreescreve a geração default para estes campos:
            dto.CodItem = MockReader.GenerateCode(10);
            dto.Quantidade = MockReader.GenerateInt32(1, 999);
            dto.Descricao = MockReader.GenerateName(4).ToUpper();

            return dto;
        }

        public override string GetStmtFindAllByCodEmpresaAndCodTransacao()
        {
            return string.Empty;
        }

        public override string GetStmtInsert()
        {
            return string.Empty;
        }
    }
}
