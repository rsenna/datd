using System;
using System.Data;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    public class OrdemServicoLenteDao : DataAccessBase<OrdemServicoLente>, IOrdemServicoLenteDao
    {
        // TODO: [STP]
        // TODO: [Detalhe Transacao] Definir a consulta de lentes por ordem de serviço.
        private const string SQL_STMT_FIND_ALL_BY_COD_EMPRESA_AND_COD_TRANSACAO = @"
            A DEFINIR;
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

        public override OrdemServicoLente[] FindAll()
        {
            throw new NotImplementedException();
        }

        public override OrdemServicoLente FindByPrimaryKey(object pk)
        {
            throw new NotImplementedException();
        }

        public OrdemServicoLente FindByTransacao(Compra compra)
        {
            OrdemServicoLente result = null;

            Helper.UsingCommand(Session.Connection, c =>
            {
                c.CommandText = SQL_STMT_FIND_ALL_BY_COD_EMPRESA_AND_COD_TRANSACAO;
                Helper.AddParameter(c, "@PCOD_CLIENTE", DbType.Int32, compra.CodCliente);
                Helper.AddParameter(c, "@PCOD_TRANSACAO", DbType.Int32, compra.CodTransacao);
                result = FetchDto(c);
            });

            return result;
        }

        public override OrdemServicoLente Update(OrdemServicoLente dto)
        {
            throw new NotImplementedException();
        }

        public OrdemServicoLente[] FindAll(int codOrdemServico)
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

            return dto; // Deveria ser atualizado com valor gerado de PK (se é que isso ocorre; pela proc pelo menos nada é indicado).
        }

        public override OrdemServicoLente FetchDto(IDataRecord record)
        {
            // TODO: [Detalhe Transacao] Definir os nomes dos campos da lente.
            return new OrdemServicoLente {
                CodEmpresa = Helper.ReadInt32(record, "").Value,
                CodTransacao = Helper.ReadInt32(record, "").Value,
                CodOrdemServicoLente = Helper.ReadInt32(record, "").Value,
                Descricao = Helper.ReadString(record, ""),
                LongeEsf = Helper.ReadDecimal(record, "").Value,
                LongeCil = Helper.ReadDecimal(record, "").Value,
                LongeEixo = Helper.ReadDecimal(record, "").Value,
                Adicao = Helper.ReadDecimal(record, "").Value,
                PertoEsf = Helper.ReadDecimal(record, "").Value,
                PertoCil = Helper.ReadDecimal(record, "").Value,
                PertoEixo = Helper.ReadDecimal(record, "").Value,
                Dnp = Helper.ReadDecimal(record, "").Value,
                Alt = Helper.ReadDecimal(record, "").Value
            };
        }
    }
}