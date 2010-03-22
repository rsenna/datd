using Dataweb.Dilab.Model.DataAccess.Contracts;
using Dataweb.Dilab.Model.DataTransfer;
using Base=Dataweb.Dilab.Model.DataAccess;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    public class TransacaoDao : Base.TransacaoDao<Transacao>, ITransacaoDao
    {
        // TODO: Transacao: Definir SQL busca por COD_CLIENTE e COD_NOTA_FISCAL.
        internal const string SQL_STMT_FIND_ALL_BY_COD_CLIENTE_AND_COD_NOTA_FISCAL = "???";

        internal const string SQL_STMT_FIND_ALL = @"
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
                STP_WEBORDEMSERVICO_CONSULTAR(@PCOD_CLIENTE, @PCOD_NOTAFISCALEMITIDA)
        ";

        internal const string SQL_STMT_FIND_ONE_BY_COD_EMPRESA_AND_COD_TRANSACAO = @"
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
        internal const string SQL_STMT_COUNT_FECHADAS = @"
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
        internal const string SQL_STMT_COUNT_EM_PRODUCAO = @"
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

        internal const string SQL_STMT_INSERT = @"
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

        internal const string SQL_STMT_CLOSE = @"
            EXECUTE PROCEDURE STP_WEBORDEMSERVICO_FECHAR(
                @PCOD_EMPRESA,
                @PCOD_ORDEMSERVICO
            )";

        public override string GetStmtFindAll()
        {
            return SQL_STMT_FIND_ALL;
        }

        public override string GetStmtFindAllByCodClienteAndCodNotaFiscal()
        {
            return SQL_STMT_FIND_ALL_BY_COD_CLIENTE_AND_COD_NOTA_FISCAL;
        }

        public override string GetStmtFindOneByCodEmpresaAndCodTransacao()
        {
            return SQL_STMT_FIND_ONE_BY_COD_EMPRESA_AND_COD_TRANSACAO;
        }

        public override string GetStmtInsert()
        {
            return SQL_STMT_INSERT;
        }

        public override string GetStmtCountFechadas()
        {
            return SQL_STMT_COUNT_FECHADAS;
        }

        public override string GetStmtCountEmProducao()
        {
            return SQL_STMT_COUNT_EM_PRODUCAO;
        }

        public override string GetStmtClose()
        {
            return SQL_STMT_CLOSE;
        }
    }
}