using System.Data;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    public class ClienteDao : DataAccessBase<Cliente>, IClienteDao
    {
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
                INNER JOIN webrascliente
                    ON (webrascliente.cod_cliente = pessoacliente.cod_pessoa)
            WHERE
                pessoacliente.cnpj = @CNPJ
        ";

        public override Cliente FetchDto(IDataRecord reader)
        {
            return new Cliente {
                CodCliente = Helper.ReadInt32(reader, "COD_CLIENTE").Value,
                Identificador = Helper.ReadInt32(reader, "IDENTIFICADOR").Value,
                Cnpj = Helper.ReadString(reader, "CNPJ"),
                Nome = Helper.ReadString(reader, "NOME"),
                Senha = Helper.ReadString(reader, "SENHA"),
                CodEmpresa = Helper.ReadInt32(reader, "COD_EMPRESA").Value,
                NomeEmpresa = Helper.ReadString(reader, "NOMEEMPRESA"),
                EmailNotificacao = Helper.ReadString(reader, "EMAILNOTIFICACAO"),
                ReceberNotificacao = Helper.ReadBoolean(reader, "RECEBERNOTIFICACAO").Value
            };
        }

        public Cliente FindByIdentificador(int identificador)
        {
            Cliente result = null;

            Helper.UsingCommand(c => {
                c.CommandText = SQL_STMT_FIND_BY_IDENTIFICADOR;

                Helper.AddParameter(c, "@IDENTIFICADOR", DbType.Int32, identificador);

                result = FetchDto(c);
            });

            return result;
        }

        public Cliente FindByCnpj(string cnpj)
        {
            Cliente result = null;

            Helper.UsingCommand(c =>
            {
                c.CommandText = SQL_STMT_FIND_BY_CNPJ;

                Helper.AddParameter(c, "@CNPJ", DbType.String, cnpj);

                result = FetchDto(c);
            });

            return result;
        }

        public override Cliente FindByPrimaryKey(object primaryKey)
        {
            Cliente result = null;

            Helper.UsingCommand(c => {
                c.CommandText = SQL_STMT_FIND_BY_PRIMARY_KEY;
                Helper.AddParameter(c, "@COD_CLIENTE", DbType.Int32, primaryKey);
                result = FetchDto(c);
            });

            return result;
        }

        public virtual Cliente FindByPrimaryKey(int codCliente)
        {
            return FindByPrimaryKey((object) codCliente);
        }

        
        /// <summary>
        /// Altera um registro da entidade Cliente
        /// </summary>
        /// <param name="dto">DTO do cliente</param>
        /// <remarks>
        /// Atualmente são alterados apenas os campos da tabela "webrascliente"
        /// </remarks>
        public override Cliente Update(Cliente dto)
        {
            Helper.UsingCommand(c =>
            {
                c.CommandText = SQL_STMT_UPDATE;

                Helper.AddParameter(c, "@EMAILNOTIFICACAO", DbType.String, dto.EmailNotificacao);
                Helper.AddParameter(c, "@RECEBERNOTIFICACAO", DbType.String, Helper.ToString(dto.ReceberNotificacao));
                Helper.AddParameter(c, "@SENHA", DbType.String, dto.Senha);

                Helper.AddParameter(c, "@COD_CLIENTE", DbType.Int32, dto.CodCliente);

                c.ExecuteNonQuery();
            });

            return dto;
        }

        public override Cliente Insert(Cliente dto)
        {
            throw new System.NotImplementedException();
        }
    }
}