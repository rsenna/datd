using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    public class TransacaoDao : TransacaoDao<Transacao>, ITransacaoDao {}

    public abstract class TransacaoDao<T> : DataAccessBase<T>, ITransacaoDao<T>
        where T : Transacao, new()
    {
        private const string SQL_STMT_FIND_ALL = @"
            SELECT
                RCODEMPRESA,
                RCODTRANSACAO,
                RNUMEROORDEMSERVICO,
                RREFERENCIA,
                RDATAHORAEMISSAO,
                RDATAHORAPREVISAO,
                RDATAHORAEXPEDICAO,
                RCODETAPA,
                RAVISOMENSAGEM,
                RTIPO
            FROM
                STP_WEBORDEMSERVICO_CONSULTAR(@PCOD_CLIENTE)
        ";

        protected const string SQL_STMT_FIND_ONE_BY_COD_EMPRESA_AND_COD_TRANSACAO = @"
            SELECT
                RCODEMPRESA,
                RCODTRANSACAO,
                RCODCLIENTE,
                RNUMEROORDEMSERVICO,
                RREFERENCIA,
                RDATAHORAEMISSAO,
                RDATAHORAPREVISAO,
                RDATAHORAEXPEDICAO,
                RCODETAPA,
                RAVISOMENSAGEM,
                RTIPO,
                RTIPOVT,
                RTA,
                RMD,
                RDIAMETRO,
                RDP,
                RAA,
                REIXO,
                RPONTE,
                RCODOTICALENTEMATERIAL,
                RDESCRICAOMATERIAL,
                RDESCRICAOARMACAO,
                ROBSERVACAOARMACAO,
                ROBSERVACAOLENTE
            FROM
                STP_WEBCONSULTARTRANSACAO(@PCOD_EMPRESA, @PCOD_TRANSACAO)
        ";

        // TODO: [STP]
        private const string SQL_STMT_COUNT_FECHADAS = @"
            SELECT
                COUNT(*)
            FROM
                transacao
            WHERE
                (tipotransacao = 'OS') AND
                (dataencerramento = CURRENT_DATE) AND
                (cod_pessoa = @PCOD_CLIENTE)
        ";

        // TODO: [STP]
        private const string SQL_STMT_COUNT_EM_PRODUCAO = @"
            SELECT
                COUNT(*)
            FROM
                oticarascaixa OTRC
                INNER JOIN oticarasordemservico OTOS ON
                    (OTOS.cod_oticarasordemservico = OTRC.cod_oticarasordemservico)
                INNER JOIN pessoa PESS ON
                    (PESS.nome = OTOS.nomecliente)
            WHERE
                (OTRC.numerocaixa IS NOT NULL) AND
                (PESS.cod_pessoa = @PCOD_CLIENTE)
        ";

        private const string SQL_STMT_INSERT = @"
            EXECUTE PROCEDURE STP_WEBORDEMSERVICO_ABRIR(
                @PCOD_CLIENTE,
                @POBSERVACAO,
                @PREFERENCIA,
                @PDESCRICAOARMACAO,
                @POBSERVACAOARMACAO,
                @PCOD_OTICALENTEMATERIAL,
                @PTIPOVT,
                @PTA,
                @PMD,
                @PDIAMETRO,
                @POBSERVACAOLENTE,
                @PDP,
                @PAA,
                @PEIXO,
                @PPONTE
            )";

        private const string SQL_STMT_CLOSE = @"
            EXECUTE PROCEDURE STP_WEBORDEMSERVICO_FECHAR(
                @PCOD_EMPRESA,
                @PCOD_ORDEMSERVICO
            )";

        public override T InitDto(IDataRecord record, T dto)
        {
            dto.CodEmpresa = Helper.ReadInt32(record, "RCODEMPRESA").Value;
            dto.CodTransacao = Helper.ReadInt32(record, "RCODTRANSACAO").Value;
            dto.Numero = Helper.ReadInt32(record, "RNUMEROORDEMSERVICO").Value;
            dto.Referencia = Helper.ReadString(record, "RREFERENCIA");
            dto.Emissao = Helper.ReadDateTime(record, "RDATAHORAEMISSAO").Value;
            dto.Previsao = Helper.ReadDateTime(record, "RDATAHORAPREVISAO");
            dto.Expedicao = Helper.ReadDateTime(record, "RDATAHORAEXPEDICAO");
            dto.Etapa = (TipoEtapa)Helper.ReadInt32(record, "RCODETAPA");
            dto.AvisoMensagem = Helper.ReadString(record, "RAVISOMENSAGEM");
            dto.Tipo = (TipoTransacao) Helper.ReadInt32(record, "RTIPO");

            if (Depth > QueryDepth.FirstLevel)
            {
                var itemTransacaoDao = new ItemTransacaoDao {Depth = GetDetailDepth(), Session = Session};
                dto.Itens = (ItemTransacao[])itemTransacaoDao.FindAll(dto.CodEmpresa, dto.CodTransacao);
            }

            return dto;
        }

        public override IEnumerable<T> FindAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FindAll(int codCliente)
        {
            var result = new List<T>();

            Helper.UsingCommand(Session.Connection, c =>
            {
                c.CommandText = SQL_STMT_FIND_ALL;
                Helper.AddParameter(c, "@PCOD_CLIENTE", DbType.Int32, codCliente);
                InitDtos(c, result);
            });

            return result;
        }

        public override T FindByPrimaryKey(object pk)
        {
            throw new NotImplementedException();
        }

        public virtual T FindByPrimaryKey(int codEmpresa, int codTransacao)
        {
            var result = new T();

            Helper.UsingCommand(Session.Connection, c =>
            {
                c.CommandText = SQL_STMT_FIND_ONE_BY_COD_EMPRESA_AND_COD_TRANSACAO;
                Helper.AddParameter(c, "@PCOD_EMPRESA", DbType.Int32, codEmpresa);
                Helper.AddParameter(c, "@PCOD_TRANSACAO", DbType.Int32, codTransacao);
                result = InitDto(c, result);
            });

            return result;
        }

        protected virtual void PrepareParameters(DbCommand c, T dto)
        {
            // Parâmetros abaixo só são utilizados para OS, portanto vão em branco:
            Helper.AddParameter(c, "@PREFERENCIA", DbType.String, DBNull.Value);
            Helper.AddParameter(c, "@PDESCRICAOARMACAO", DbType.String, DBNull.Value);
            Helper.AddParameter(c, "@POBSERVACAOARMACAO", DbType.String, DBNull.Value);
            Helper.AddParameter(c, "@PCOD_OTICALENTEMATERIAL", DbType.Int32, DBNull.Value);
            Helper.AddParameter(c, "@PTIPOVT", DbType.Int32, 0);
            Helper.AddParameter(c, "@PTA", DbType.Decimal, DBNull.Value);
            Helper.AddParameter(c, "@PMD", DbType.Decimal, DBNull.Value);
            Helper.AddParameter(c, "@PDIAMETRO", DbType.Decimal, DBNull.Value);
            Helper.AddParameter(c, "@POBSERVACAOLENTE", DbType.String, DBNull.Value);
            Helper.AddParameter(c, "@PDP", DbType.Decimal, DBNull.Value);
            Helper.AddParameter(c, "@PAA", DbType.Decimal, DBNull.Value);
            Helper.AddParameter(c, "@PEIXO", DbType.Decimal, DBNull.Value);
            Helper.AddParameter(c, "@PPONTE", DbType.Decimal, DBNull.Value);
        }

        public override T Insert(T dto)
        {
            Helper.UsingCommand(Session.Connection, c => {
                c.CommandText = SQL_STMT_INSERT;

                // Entrada:
                Helper.AddParameter(c, "@PCOD_CLIENTE", DbType.Int32, dto.CodCliente);
                Helper.AddParameter(c, "@POBSERVACAO", DbType.String, dto.Observacao);
                Helper.AddParameter(c, "@PREFERENCIA", DbType.String, dto.Referencia);

                PrepareParameters(c, dto);

                // Saída:
                var paramCodEmpresa = Helper.AddReturnParameter(c, "@RCOD_EMPRESA", DbType.Int32);
                var paramCodOs = Helper.AddReturnParameter(c, "@RCOD_ORDEMSERVICO", DbType.Int32);
                var paramNumero = Helper.AddReturnParameter(c, "@RNUMEROORDEMSERVICO", DbType.Int32);

                c.ExecuteNonQuery();

                dto.CodEmpresa = (int)paramCodEmpresa.Value;
                dto.CodTransacao = (int)paramCodOs.Value;
                dto.Numero = (int)paramNumero.Value;
            });

            if (dto.Itens != null)
            {
                foreach (var item in dto.Itens)
                {
                    item.CodTransacao = dto.CodTransacao;
                    item.CodEmpresa = dto.CodEmpresa;
                }

                // Grava os serviços que serão executados na OS:
                var itemTransacaoDao = new ItemTransacaoDao {Session = Session};
                itemTransacaoDao.Insert(dto.Itens);
            }

            return dto;
        }

        public override T Update(T dto)
        {
            throw new NotImplementedException();
        }

        protected int ExecuteScalarInt32(string sqlStmt, int codCliente)
        {
            var result = 0;

            Helper.UsingCommand(Session.Connection, c => {
                c.CommandText = sqlStmt;
                Helper.AddParameter(c, "@PCOD_CLIENTE", DbType.Int32, codCliente);
                result = Convert.ToInt32(c.ExecuteScalar());
            });

            return result;
        }

        public int GetCountFechadas(int codCliente)
        {
            return ExecuteScalarInt32(SQL_STMT_COUNT_FECHADAS, codCliente);
        }

        public int GetCountEmProducao(int codCliente)
        {
            return ExecuteScalarInt32(SQL_STMT_COUNT_EM_PRODUCAO, codCliente);
        }

        public T Close(T dto)
        {
            Helper.UsingCommand(Session.Connection, c =>
            {
                c.CommandText = SQL_STMT_CLOSE;

                // Entrada:
                Helper.AddParameter(c, "@PCOD_EMPRESA", DbType.Int32, dto.CodCliente);
                Helper.AddParameter(c, "@PCOD_ORDEMSERVICO", DbType.Int32, dto.CodTransacao);

                c.ExecuteNonQuery();
            });

            return dto;
        }
    }
}