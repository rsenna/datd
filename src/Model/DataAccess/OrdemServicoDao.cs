using System.Data;
using Dataweb.Dilab.Model.DataAccess.Contracts;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess
{
    public abstract class OrdemServicoDao : TransacaoDao<OrdemServico>, IOrdemServicoDao
    {
        public override OrdemServico InitDto(IReader reader, OrdemServico dto)
        {
            base.InitDto(reader, dto);

            dto.DescricaoArmacao = reader.ReadOptional("RDESCRICAOARMACAO");
            dto.ObservacaoArmacao = reader.ReadOptional("ROBSERVACAOARMACAO");
            dto.CodMaterial = reader.ReadOptional<int>("RCODOTICALENTEMATERIAL");
            dto.TipoVt = reader.ReadOptional<int>("RTIPOVT");
            dto.Ta = reader.ReadOptional<decimal>("RTA");
            dto.Md = reader.ReadOptional<decimal>("RMD");
            dto.Diametro = reader.ReadOptional<decimal>("RDIAMETRO");
            dto.ObservacaoLente = reader.ReadOptional("ROBSERVACAOLENTE");
            dto.Dp = reader.ReadOptional<decimal>("RDP");
            dto.Aa = reader.ReadOptional<decimal>("RAA");
            dto.Eixo = reader.ReadOptional<decimal>("REIXO");
            dto.Ponte = reader.ReadOptional<decimal>("RPONTE");

            if (Depth > QueryDepth.FirstLevel)
            {
                var lenteDao = DataAccessFactory.CreateDao<IOrdemServicoLenteDao>(this);
                dto.LenteOd = lenteDao.FindByPrimaryKey(dto.CodEmpresa, dto.CodTransacao, TipoLente.OlhoDireito);
                dto.LenteOe = lenteDao.FindByPrimaryKey(dto.CodEmpresa, dto.CodTransacao, TipoLente.OlhoEsquerdo);
            }

            return dto;
        }

        // TODO: Falta passar campo dto.Observacao (OBSERVACAOGERAL)
        protected override void PrepareParameters(ICommand c, OrdemServico dto)
        {
            c.AddParameter("@PDESCRICAOARMACAO", DbType.String, dto.DescricaoArmacao);
            c.AddParameter("@POBSERVACAOARMACAO", DbType.String, dto.ObservacaoArmacao);
            c.AddParameter("@PCOD_OTICALENTEMATERIAL", DbType.Int32, dto.CodMaterial);
            c.AddParameter("@PTIPOVT", DbType.Int32, dto.TipoVt);
            c.AddParameter("@PTA", DbType.Decimal, dto.Ta);
            c.AddParameter("@PMD", DbType.Decimal, dto.Md);
            c.AddParameter("@PDIAMETRO", DbType.Decimal, dto.Diametro);
            c.AddParameter("@POBSERVACAOLENTE", DbType.String, dto.ObservacaoLente);
            c.AddParameter("@PDP", DbType.Decimal, dto.Dp);
            c.AddParameter("@PAA", DbType.Decimal, dto.Aa);
            c.AddParameter("@PEIXO", DbType.Decimal, dto.Eixo);
            c.AddParameter("@PPONTE", DbType.Decimal, dto.Ponte);
        }

        private void InsertLente(Transacao dtoOs, OrdemServicoLente dtoLente)
        {
            var ordemServicoLenteDao = DataAccessFactory.CreateDao<IOrdemServicoLenteDao>(this);

            // Como só agora foi obtido dto.CodTransacao e dto.CodEmpresa, atualizo dto demais DTO's:
            dtoLente.CodTransacao = dtoOs.CodTransacao;
            dtoLente.CodEmpresa = dtoOs.CodEmpresa;

            // NOTE: Por enquanto a descrição da lente vai ser igual ao nome da família - isso irá mudar no futuro.
            var familiaDao = DataAccessFactory.CreateDao<IFamiliaDao>(this);
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