using Base=Dataweb.Dilab.Model.DataAccess;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    public class MaterialDao : Base.MaterialDao
    {
        // TODO: [STP]
        private const string SQL_STMT_FIND_ALL = @"
            SELECT
                cod_oticalentematerial,
                descricao
            FROM
                oticalentematerial
            ORDER BY
                descricao
        ";

        public override string GetStmtFindAll()
        {
            return SQL_STMT_FIND_ALL;
        }
    }
}