using Base=Dataweb.Dilab.Model.DataAccess;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    public class ClienteDao : Base.ClienteDao
    {
        // TODO: [STP]
        private const string SQL_STMT_FIND_BY_CNPJ = @"
            SELECT
                webrascliente.cod_cliente,
                webrascliente.emailnotificacao,
                webrascliente.recebernotificacao,
                webrascliente.senha,
                pessoacliente.nome,
                pessoacliente.cnpj,
                pessoacliente.identificador,
                COALESCE(pessoacliente.cod_empresa, (SELECT MIN(cfgprincipal.cod_empresamatriz) FROM cfgprincipal)) cod_empresa,
                (SELECT nome FROM pessoa WHERE cod_pessoa = COALESCE(pessoacliente.cod_empresa, (SELECT MIN(cfgprincipal.cod_empresamatriz) FROM cfgprincipal))) nomeempresa
            FROM
                pessoa pessoacliente
                INNER JOIN webrascliente ON
                    webrascliente.cod_cliente = pessoacliente.cod_pessoa
            WHERE
                pessoacliente.cnpj = @CNPJ
        ";

        // TODO: [STP]
        private const string SQL_STMT_FIND_BY_IDENTIFICADOR = @"
            SELECT
                webrascliente.cod_cliente,
                webrascliente.emailnotificacao,
                webrascliente.recebernotificacao,
                webrascliente.senha,
                pessoacliente.nome,
                pessoacliente.cnpj,
                pessoacliente.identificador,
                COALESCE(pessoacliente.cod_empresa, (SELECT MIN(cfgprincipal.cod_empresamatriz) FROM cfgprincipal)) cod_empresa,
                (SELECT nome FROM pessoa WHERE cod_pessoa = COALESCE(pessoacliente.cod_empresa, (SELECT MIN(cfgprincipal.cod_empresamatriz) FROM cfgprincipal))) nomeempresa
            FROM
                pessoa pessoacliente
                INNER JOIN webrascliente
                    ON (webrascliente.cod_cliente = pessoacliente.cod_pessoa)
            WHERE
                pessoacliente.identificador = @IDENTIFICADOR
        ";

        // TODO: [STP]
        private const string SQL_STMT_FIND_BY_PRIMARY_KEY = @"
            SELECT
                webrascliente.cod_cliente,
                webrascliente.emailnotificacao,
                webrascliente.recebernotificacao,
                webrascliente.senha,
                pessoacliente.nome,
                pessoacliente.cnpj,
                pessoacliente.identificador,
                COALESCE(pessoacliente.cod_empresa, (SELECT MIN(cfgprincipal.cod_empresamatriz) FROM cfgprincipal)) cod_empresa,
                (SELECT nome FROM pessoa WHERE cod_pessoa = COALESCE(pessoacliente.cod_empresa, (SELECT MIN(cfgprincipal.cod_empresamatriz) FROM cfgprincipal))) nomeempresa
            FROM
                pessoa pessoacliente
                INNER JOIN webrascliente
                    ON (webrascliente.cod_cliente = pessoacliente.cod_pessoa)
            WHERE
                webrascliente.cod_cliente = @COD_CLIENTE
        ";

        // TODO: [STP]
        private const string SQL_STMT_UPDATE = @"
            UPDATE
                webrascliente
            SET
                emailnotificacao = @EMAILNOTIFICACAO,
                recebernotificacao = @RECEBERNOTIFICACAO,
                senha = @SENHA
            WHERE
                cod_cliente = @COD_CLIENTE
        ";

        public override string GetStmtFindByIdentificador()
        {
            return SQL_STMT_FIND_BY_IDENTIFICADOR;
        }

        public override string GetStmtFindByCnpj()
        {
            return SQL_STMT_FIND_BY_CNPJ;
        }

        public override string GetStmtFindByPrimaryKey()
        {
            return SQL_STMT_FIND_BY_PRIMARY_KEY;
        }

        public override string GetStmtUpdate()
        {
            return SQL_STMT_UPDATE;
        }
    }
}