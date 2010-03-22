using Base=Dataweb.Dilab.Model.DataAccess;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    public class ItemDao : Base.ItemDao
    {
        // TODO: Como não usa mais tabela estoque, não dá para verificar se estoque.cod_estoquelocal = 1 - verificar se está tudo bem desta forma.
        // TODO: [STP]
        private const string SQL_STMT_FIND_ALL_PRODUTO_BY_COD_FAMILIA = @"
            SELECT
                ITEM.cod_item,
                PROD.codigobarra,
                'F' AS obrigatorio,
                ITEM.descricao,
                'N' AS servicointerno,
                1 AS tipo
            FROM
                item ITEM
                INNER JOIN produto PROD ON
                    ITEM.cod_item = PROD.cod_produto
                INNER JOIN produtofamilia PRFA ON
                    PRFA.cod_produtofamilia = PROD.cod_produtofamilia
            WHERE
                PRFA.cod_produtofamilia = @COD_PRODUTOFAMILIA AND
                ITEM.ativo = 'T'
            ORDER BY
                ITEM.descricao
        ";

        // TODO: [STP]
        private const string SQL_STMT_FIND_ALL_SERVICO_BY_COD_FAMILIA = @"
            SELECT
                ITEM.cod_item,
                NULL AS codigobarra,
                PRFS.obrigatorio,
                ITEM.descricao,
                SERV.servicointerno,
                2 AS tipo
            FROM
                produtofamilia_servico PRFS
                INNER JOIN servico SERV ON
                    SERV.cod_servico = PRFS.cod_servico
                INNER JOIN item ITEM ON
                    ITEM.cod_item = SERV.cod_servico
            WHERE
                PRFS.cod_produtofamilia = @COD_PRODUTOFAMILIA
            ORDER BY
                ITEM.descricao
        ";

        public override string GetStmtFindAllProdutoByCodFamilia()
        {
            return SQL_STMT_FIND_ALL_PRODUTO_BY_COD_FAMILIA;
        }

        public override string GetStmtFindAllServicoByCodFamilia()
        {
            return SQL_STMT_FIND_ALL_SERVICO_BY_COD_FAMILIA;
        }
    }
}