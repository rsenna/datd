using Base=Dataweb.Dilab.Model.DataAccess;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    public class FamiliaDao : Base.FamiliaDao
    {
        // TODO: [STP]
        private const string SQL_STMT_FIND_BY_PRIMARY_KEY = @"
            SELECT
                PRFA.cod_produtofamilia,
                PRFA.descricao
            FROM
                produtofamilia PRFA
            WHERE
                PRFA.cod_produtofamilia = @PCOD_PRODUTOFAMILIA
        ";

        // TODO: [STP]
        private const string SQL_STMT_FIND_ALL = @"
            SELECT
                PRFA.cod_produtofamilia,
                PRFA.descricao
            FROM
                produtofamilia PRFA
            WHERE
                PRFA.ativo = 'T'
            ORDER BY
                PRFA.descricao
        ";

        public override string GetStmtFindByPrimaryKey()
        {
            return SQL_STMT_FIND_BY_PRIMARY_KEY;
        }

        public override string GetStmtFindAll()
        {
            return SQL_STMT_FIND_ALL;
        }
    }
}