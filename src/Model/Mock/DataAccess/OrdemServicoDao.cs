using Dataweb.Dilab.Model.DataTransfer;
using Base=Dataweb.Dilab.Model.DataAccess;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class OrdemServicoDao : Base.OrdemServicoDao
    {
        public override OrdemServico InitDto(IReader reader, OrdemServico dto)
        {
            base.InitDto(reader, dto);

            // Sobreescreve a geração default para estes campos:
            dto.DescricaoArmacao = MockReader.GenerateParagraph();
            dto.ObservacaoArmacao = MockReader.GenerateParagraph();
            dto.Ta = MockReader.GenerateDecimal(-9999.999m, 9999.999m, "###0.000");
            dto.Md = MockReader.GenerateDecimal(-9999.999m, 9999.999m, "###0.000");
            dto.Diametro = MockReader.GenerateDecimal(-9999.999m, 9999.999m, "###0.000");
            dto.ObservacaoLente = MockReader.GenerateParagraph();
            dto.Dp = MockReader.GenerateDecimal(-99.9m, 99.9m, "#0.0");
            dto.Aa = MockReader.GenerateDecimal(-9999.999m, 9999.999m, "###0.000");
            dto.Eixo = MockReader.GenerateDecimal(0, 180, "##0");
            dto.Ponte = MockReader.GenerateDecimal(-9999.999m, 9999.999m, "###0.000");

            return dto;
        }

        public override string GetStmtFindAll()
        {
            return string.Empty;
        }

        public override string GetStmtFindAllByCodClienteAndCodNotaFiscal()
        {
            return string.Empty;
        }

        public override string GetStmtFindOneByCodEmpresaAndCodTransacao()
        {
            return string.Empty;
        }

        public override string GetStmtInsert()
        {
            return string.Empty;
        }

        public override string GetStmtCountFechadas()
        {
            return string.Empty;
        }

        public override string GetStmtCountEmProducao()
        {
            return string.Empty;
        }

        public override string GetStmtClose()
        {
            return string.Empty;
        }
    }
}