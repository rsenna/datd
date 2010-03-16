using System.Data;
using System.Data.Common;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    public class OrdemServicoDao : TransacaoDao<OrdemServico>, IOrdemServicoDao
    {
        public override OrdemServico InitDto(IDataRecord record, OrdemServico dto)
        {
            dto.DescricaoArmacao = Helper.ReadString(record, "RDESCRICAOARMACAO");
            dto.ObservacaoArmacao = Helper.ReadString(record, "ROBSERVACAOARMACAO");
            dto.CodMaterial = Helper.ReadInt32(record, "RCODOTICALENTEMATERIAL").Value;
            dto.TipoVt = Helper.ReadInt32(record, "RTIPOVT").Value;
            dto.Ta = Helper.ReadDecimal(record, "RTA").Value;
            dto.Md = Helper.ReadDecimal(record, "RMD").Value;
            dto.Diametro = Helper.ReadDecimal(record, "RDIAMETRO").Value;
            dto.ObservacaoLente = Helper.ReadString(record, "ROBSERVACAOLENTE");
            dto.Dp = Helper.ReadDecimal(record, "RDP").Value;
            dto.Aa = Helper.ReadDecimal(record, "RAA").Value;
            dto.Eixo = Helper.ReadDecimal(record, "REIXO").Value;
            dto.Ponte = Helper.ReadDecimal(record, "RPONTE").Value;

            if (Depth > QueryDepth.FirstLevel)
            {
                var lenteDao = new OrdemServicoLenteDao {Session = Session};
                dto.LenteOd = lenteDao.FindByPrimaryKey(dto.CodEmpresa, dto.CodTransacao, TipoLente.OlhoDireito);
                dto.LenteOe = lenteDao.FindByPrimaryKey(dto.CodEmpresa, dto.CodTransacao, TipoLente.OlhoEsquerdo);
            }

            return dto;
        }

        protected override void PrepareParameters(DbCommand c, OrdemServico dto)
        {
            Helper.AddParameter(c, "@PDESCRICAOARMACAO", DbType.String, dto.DescricaoArmacao);
            Helper.AddParameter(c, "@POBSERVACAOARMACAO", DbType.String, dto.ObservacaoArmacao);
            Helper.AddParameter(c, "@PCOD_OTICALENTEMATERIAL", DbType.Int32, dto.CodMaterial);
            Helper.AddParameter(c, "@PTIPOVT", DbType.Int32, dto.TipoVt);
            Helper.AddParameter(c, "@PTA", DbType.Decimal, dto.Ta);
            Helper.AddParameter(c, "@PMD", DbType.Decimal, dto.Md);
            Helper.AddParameter(c, "@PDIAMETRO", DbType.Decimal, dto.Diametro);
            Helper.AddParameter(c, "@POBSERVACAOLENTE", DbType.String, dto.ObservacaoLente);
            Helper.AddParameter(c, "@PDP", DbType.Decimal, dto.Dp);
            Helper.AddParameter(c, "@PAA", DbType.Decimal, dto.Aa);
            Helper.AddParameter(c, "@PEIXO", DbType.Decimal, dto.Eixo);
            Helper.AddParameter(c, "@PPONTE", DbType.Decimal, dto.Ponte);
        }

        private void InsertLente(Transacao dtoOs, OrdemServicoLente dtoLente)
        {
            var ordemServicoLenteDao = new OrdemServicoLenteDao {Depth = GetDetailDepth(), Session = Session};

            // Como só agora foi obtido dto.CodTransacao e dto.CodEmpresa, atualizo dto demais DTO's:
            dtoLente.CodTransacao = dtoOs.CodTransacao;
            dtoLente.CodEmpresa = dtoOs.CodEmpresa;

            // NOTE: Por enquanto a descrição da lente vai ser igual ao nome da família - isso irá mudar no futuro.
            var familiaDao = new FamiliaDao {Depth = GetDetailDepth(), Session = Session};
            var familia = familiaDao.FindByPrimaryKey(dtoLente.CodFamilia);
            dtoLente.Descricao = familia.Descricao;

            // Grava lentes:
            ordemServicoLenteDao.Insert(dtoLente);
        }

        public override OrdemServico Insert(OrdemServico dto)
        {
            dto = base.Insert(dto);

            InsertLente(dto, dto.LenteOd);
            InsertLente(dto, dto.LenteOe);

            return dto;
        }
    }
}
