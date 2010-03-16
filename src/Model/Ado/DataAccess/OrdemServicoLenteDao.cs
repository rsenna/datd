using System;
using System.Collections.Generic;
using System.Data;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    public class OrdemServicoLenteDao : DataAccessBase<OrdemServicoLente>, IOrdemServicoLenteDao
    {
        // TODO: [STP]
        private const string SQL_STMT_FIND_ALL_BY_COD_EMPRESA_AND_COD_TRANSACAO = @"
            SELECT
                OSOL.cod_empresa,
                OSOL.cod_ordemservicootica,
                OSOL.cod_ordemservicooticalente,
                PROD.cod_produtofamilia,
                OSOL.descricaolente,
                OSOL.longe_esf,
                OSOL.longe_cil,
                OSOL.longe_eixo,
                OSOL.adicao,
                OSOL.perto_esf,
                OSOL.perto_cil,
                OSOL.perto_eixo,
                OSOL.dnp,
                OSOL.alt
            FROM
                otiordemservicootica_lente OSOL
                INNER JOIN produto PROD ON
                    PROD.cod_produto = OSOL.cod_produtolente
            WHERE
                OSOL.cod_empresa = @PCOD_EMPRESA AND
                OSOL.cod_ordemservicootica = @PCOD_ORDEMSERVICOOTICA AND
                OSOL.cod_ordemservicooticalente = @PCOD_ORDEMSERVICOOTICALENTE
        ";

        private const string SQL_STMT_INSERT = @"
            EXECUTE PROCEDURE STP_WEBORDEMSERVICO_ADDLENTE(
                @PCOD_EMPRESA,
                @PCOD_ORDEMSERVICO,
                @PCOD_ORDEMSERVICOOTICALENTE,
                @PDESCRICAOLENTE,
                @PLONGE_ESF,
                @PLONGE_CIL,
                @PLONGE_EIXO,
                @PADICAO,
                @PPERTO_ESF,
                @PPERTO_CIL,
                @PPERTO_EIXO,
                @PDNP,
                @PALT
            )";

        public override IEnumerable<OrdemServicoLente> FindAll()
        {
            throw new NotImplementedException();
        }

        public override OrdemServicoLente FindByPrimaryKey(object pk)
        {
            throw new NotImplementedException();
        }

        public OrdemServicoLente FindByPrimaryKey(int codEmpresa, int codTransacao, TipoLente tipoLente)
        {
            var result = new OrdemServicoLente();

            Helper.UsingCommand(Session.Connection, c =>
            {
                c.CommandText = SQL_STMT_FIND_ALL_BY_COD_EMPRESA_AND_COD_TRANSACAO;

                Helper.AddParameter(c, "@PCOD_EMPRESA", DbType.Int32, codEmpresa);
                Helper.AddParameter(c, "@PCOD_ORDEMSERVICOOTICA", DbType.Int32, codTransacao);
                Helper.AddParameter(c, "@PCOD_ORDEMSERVICOOTICALENTE", DbType.Int32, (int) tipoLente);

                InitDto(c, result);
            });

            return result;
        }

        public override OrdemServicoLente Update(OrdemServicoLente dto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrdemServicoLente> FindAll(int codOrdemServico)
        {
            throw new NotImplementedException();
        }

        public override OrdemServicoLente Insert(OrdemServicoLente dto)
        {
            Helper.UsingCommand(Session.Connection, c => {
                c.CommandText = SQL_STMT_INSERT;

                Helper.AddParameter(c, "@PCOD_EMPRESA", DbType.Int32, dto.CodEmpresa);
                Helper.AddParameter(c, "@PCOD_ORDEMSERVICO", DbType.Int32, dto.CodTransacao);
                Helper.AddParameter(c, "@PCOD_ORDEMSERVICOOTICALENTE", DbType.Int32, (int) dto.TipoLente);
                Helper.AddParameter(c, "@PDESCRICAOLENTE", DbType.String, dto.Descricao);
                Helper.AddParameter(c, "@PLONGE_ESF", DbType.Decimal, dto.LongeEsf);
                Helper.AddParameter(c, "@PLONGE_CIL", DbType.Decimal, dto.LongeCil);
                Helper.AddParameter(c, "@PLONGE_EIXO", DbType.Decimal, dto.LongeEixo);
                Helper.AddParameter(c, "@PADICAO", DbType.Decimal, dto.Adicao);
                Helper.AddParameter(c, "@PPERTO_ESF", DbType.Decimal, dto.PertoEsf);
                Helper.AddParameter(c, "@PPERTO_CIL", DbType.Decimal, dto.PertoCil);
                Helper.AddParameter(c, "@PPERTO_EIXO", DbType.Decimal, dto.PertoEixo);
                Helper.AddParameter(c, "@PDNP", DbType.Decimal, dto.Dnp);
                Helper.AddParameter(c, "@PALT", DbType.Decimal, dto.Alt);

                c.ExecuteNonQuery();
            });

            return dto;
        }

        public override OrdemServicoLente InitDto(IDataRecord record, OrdemServicoLente dto)
        {
            dto.CodEmpresa = Helper.ReadInt32(record, "cod_empresa").Value;
            dto.CodTransacao = Helper.ReadInt32(record, "cod_ordemservicootica").Value;
            dto.TipoLente = (TipoLente) Helper.ReadInt32(record, "cod_ordemservicooticalente").Value;
            dto.Descricao = Helper.ReadString(record, "descricaolente");
            dto.LongeEsf = Helper.ReadDecimal(record, "longe_esf").Value;
            dto.LongeCil = Helper.ReadDecimal(record, "longe_cil").Value;
            dto.LongeEixo = Helper.ReadDecimal(record, "longe_eixo").Value;
            dto.Adicao = Helper.ReadDecimal(record, "adicao").Value;
            dto.PertoEsf = Helper.ReadDecimal(record, "perto_esf").Value;
            dto.PertoCil = Helper.ReadDecimal(record, "perto_cil").Value;
            dto.PertoEixo = Helper.ReadDecimal(record, "perto_eixo").Value;
            dto.Dnp = Helper.ReadDecimal(record, "dnp").Value;
            dto.Alt = Helper.ReadDecimal(record, "alt").Value;

            return dto;
        }
    }
}