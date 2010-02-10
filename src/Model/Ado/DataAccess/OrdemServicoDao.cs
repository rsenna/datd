﻿using System;
using System.Data;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    public class OrdemServicoDao : DataAccessBase<OrdemServico>, IOrdemServicoDao
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
                RAVISOMENSAGEM
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

        public override OrdemServico FetchDto(IDataRecord reader)
        {
            return new OrdemServico {
                CodEmpresa = Helper.ReadInt32(reader, "RCODEMPRESA").Value,
                CodTransacao = Helper.ReadInt32(reader, "RCODTRANSACAO").Value,
                NumeroOrdemServico = Helper.ReadInt32(reader, "RNUMEROORDEMSERVICO").Value,
                Referencia = Helper.ReadString(reader, "RREFERENCIA"),
                Emissao = Helper.ReadDateTime(reader, "RDATAHORAEMISSAO").Value,
                Previsao = Helper.ReadDateTime(reader, "RDATAHORAPREVISAO"),
                Expedicao = Helper.ReadDateTime(reader, "RDATAHORAEXPEDICAO"),
                Etapa = (TipoEtapa)Helper.ReadInt32(reader, "RCODETAPA"),
                AvisoMensagem = Helper.ReadString(reader, "RAVISOMENSAGEM")
            };
        }

        public override OrdemServico FindByPrimaryKey(object pk)
        {
            throw new NotImplementedException();
        }

        public override void Update(OrdemServico dto)
        {
            throw new NotImplementedException();
        }

        public OrdemServico[] FindAll(int codCliente)
        {
            OrdemServico[] result = null;

            Helper.UsingCommand(c =>
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

            Helper.UsingCommand(c =>
            {
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
    }
}