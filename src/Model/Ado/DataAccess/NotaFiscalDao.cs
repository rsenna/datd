using System;
using Base=Dataweb.Dilab.Model.DataAccess;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    public class NotaFiscalDao : Base.NotaFiscalDao
    {
        private const string SQL_STMT_FIND_ALL_BY_COD_CLIENTE = @"
            select
                RCOD_NOTAFISCALEMITIDA,
                RCOD_PESSOA,
                RCOD_FATURATRANSACAO,
                RNUMERONOTAFISCAL,
                RDATAEMISSAO,
                RTOTAL,
                RNFE
            from
                STP_WEBCONSULTARNOTASFISCAIS(@PCOD_CLIENTE, NULL)
        ";

        private const string SQL_STMT_FIND_ALL_BY_COD_CLIENTE_AND_COD_FATURA = @"
            select
                RCOD_NOTAFISCALEMITIDA,
                RCOD_PESSOA,
                RCOD_FATURATRANSACAO,
                RNUMERONOTAFISCAL,
                RDATAEMISSAO,
                RTOTAL,
                RNFE
            from
                STP_WEBCONSULTARNOTASFISCAIS(@PCOD_CLIENTE, @PCOD_FATURA)
        ";

        private const string SQL_STMT_FIND_BY_PRIMARY_KEY = @"
            select
                RCOD_NOTAFISCALEMITIDA,
                RCOD_PESSOA,
                RCOD_FATURATRANSACAO,
                RNUMERONOTAFISCAL,
                RDATAEMISSAO,
                RTOTAL,
                RNFE
            from
                STP_WEBCONSULTARNOTASFISCAL(@PCOD_NOTAFISCAL)
        ";

        public override string GetStmtFindAllByCodCliente()
        {
            return SQL_STMT_FIND_ALL_BY_COD_CLIENTE;
        }

        public override string GetStmtFindAllByCodClienteAndCodFatura()
        {
            return SQL_STMT_FIND_ALL_BY_COD_CLIENTE_AND_COD_FATURA;
        }

        public override string GetStmtFindByPrimaryKey()
        {
            return SQL_STMT_FIND_BY_PRIMARY_KEY;
        }

        // TODO: NotaFiscal: Definir como buscar XML
        public override string GetXml(int codNotaFiscal)
        {
            throw new NotImplementedException();
        }
    }
}