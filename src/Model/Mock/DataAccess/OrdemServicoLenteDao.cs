using Dataweb.Dilab.Model.DataTransfer;
using Base=Dataweb.Dilab.Model.DataAccess;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class OrdemServicoLenteDao : Base.OrdemServicoLenteDao
    {
        public override OrdemServicoLente InitDto(IReader reader, OrdemServicoLente dto)
        {
            base.InitDto(reader, dto);

            // Sobreescreve a geração default para estes campos:
            dto.Descricao = MockReader.GenerateName(3);
            dto.LongeEsf = MockReader.GenerateDecimal(-9999.99m, 9999.99m, "###0.00");
            dto.LongeCil = MockReader.GenerateDecimal(-9.99m, 9.99m, "0.00");
            dto.LongeEixo = MockReader.GenerateDecimal(0, 180, "##0");
            dto.Adicao = MockReader.GenerateDecimal(-9.99m, 9.99m, "0.00");
            dto.PertoEsf = MockReader.GenerateDecimal(-9999.99m, 9999.99m, "###0.00");
            dto.PertoCil = MockReader.GenerateDecimal(-9.99m, 9.99m, "0.00");
            dto.PertoEixo = MockReader.GenerateDecimal(0, 180, "##0");
            dto.Dnp = MockReader.GenerateDecimal(-99.9m, 99.9m, "#0.0");
            dto.Alt = MockReader.GenerateDecimal(-9999.99m, 9999.99m, "###0.00");

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

        public override OrdemServicoLente FindByPrimaryKey(int codEmpresa, int codTransacao, TipoLente tipoLente)
        {
            var dto = base.FindByPrimaryKey(codEmpresa, codTransacao, tipoLente);
            dto.TipoLente = tipoLente;
            return dto;
        }
    }
}