using System;
using System.Data;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    public class CompraDao : DataAccessBase<Compra>, ICompraDao
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

        public override Compra FetchDto(IDataRecord record)
        {
            return new Compra {
                CodEmpresa = Helper.ReadInt32(record, "RCODEMPRESA").Value,
                CodTransacao = Helper.ReadInt32(record, "RCODTRANSACAO").Value,
                Numero = Helper.ReadInt32(record, "RNUMEROORDEMSERVICO").Value,
                Referencia = Helper.ReadString(record, "RREFERENCIA"),
                Emissao = Helper.ReadDateTime(record, "RDATAHORAEMISSAO").Value,
                Previsao = Helper.ReadDateTime(record, "RDATAHORAPREVISAO"),
                Expedicao = Helper.ReadDateTime(record, "RDATAHORAEXPEDICAO"),
                Etapa = (TipoEtapa)Helper.ReadInt32(record, "RCODETAPA"),
                AvisoMensagem = Helper.ReadString(record, "RAVISOMENSAGEM"),
                Tipo = (TipoCompra)Helper.ReadInt32(record, "RTIPO")
            };
        }

        public override Compra[] FindAll()
        {
            throw new NotImplementedException();
        }

        public override Compra FindByPrimaryKey(object pk)
        {
            throw new NotImplementedException();
        }

        public override Compra Update(Compra dto)
        {
            throw new NotImplementedException();
        }

        public Compra[] FindAll(int codCliente)
        {
            Compra[] result = null;

            Helper.UsingCommand(Connection, c =>
            {
                c.CommandText = SQL_STMT_FIND_ALL;
                Helper.AddParameter(c, "@PCOD_CLIENTE", DbType.Int32, codCliente);
                result = FetchDtos(c);
            });

            return result;
        }

        protected int ExecuteScalarInt32(string sqlStmt, int codCliente)
        {
            var result = 0;

            Helper.UsingCommand(Connection, c => {
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

        public override Compra Insert(Compra dto)
        {
            throw new NotImplementedException();
        }
    }
}