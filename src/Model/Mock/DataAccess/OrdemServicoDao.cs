using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class OrdemServicoDao : TransacaoDao<OrdemServico>, IOrdemServicoDao
    {
        public override OrdemServico InitDto(OrdemServico dto)
        {
            base.InitDto(dto);

            dto.DescricaoArmacao = GenerateParagraph();
            dto.ObservacaoArmacao = GenerateParagraph();
            dto.CodMaterial = GenerateInt32();
            dto.TipoVt = GenerateInt32();
            dto.Ta = GenerateDecimal(-9999.999m, 9999.999m, "###0.000");
            dto.Md = GenerateDecimal(-9999.999m, 9999.999m, "###0.000");
            dto.Diametro = GenerateDecimal(-9999.999m, 9999.999m, "###0.000");
            dto.ObservacaoLente = GenerateParagraph();
            dto.Dp = GenerateDecimal(-99.9m, 99.9m, "#0.0");
            dto.Aa = GenerateDecimal(-9999.999m, 9999.999m, "###0.000");
            dto.Eixo = GenerateDecimal(0, 180, "##0");
            dto.Ponte = GenerateDecimal(-9999.999m, 9999.999m, "###0.000");

            if (Depth > QueryDepth.FirstLevel)
            {
                var lenteDao = new OrdemServicoLenteDao {Depth = GetDetailDepth()};
                dto.LenteOd = lenteDao.FindByPrimaryKey(dto.CodEmpresa, dto.CodTransacao, TipoLente.OlhoDireito);
                dto.LenteOe = lenteDao.FindByPrimaryKey(dto.CodEmpresa, dto.CodTransacao, TipoLente.OlhoEsquerdo);
            }

            return dto;
        }
    }
}