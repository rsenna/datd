using System;
using System.Collections.Generic;
using System.Data;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    public class ClienteDao : DataAccessBase<Cliente>, IClienteDao
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

        public Cliente FindByIdentificador(int identificador)
        {
            var result = new Cliente();

            Helper.UsingCommand(Session.Connection, c => {
                c.CommandText = SQL_STMT_FIND_BY_IDENTIFICADOR;

                Helper.AddParameter(c, "@IDENTIFICADOR", DbType.Int32, identificador);

                result = InitDto(c, result);
            });

            return result;
        }

        public Cliente FindByCnpj(string cnpj)
        {
            var result = new Cliente();

            Helper.UsingCommand(Session.Connection, c => {
                c.CommandText = SQL_STMT_FIND_BY_CNPJ;

                Helper.AddParameter(c, "@CNPJ", DbType.String, cnpj);

                InitDto(c, result);
            });

            return result;
        }

        public override IEnumerable<Cliente> FindAll()
        {
            throw new NotImplementedException();
        }

        public override Cliente FindByPrimaryKey(object pk)
        {
            var result = new Cliente();

            Helper.UsingCommand(Session.Connection, c => {
                c.CommandText = SQL_STMT_FIND_BY_PRIMARY_KEY;
                Helper.AddParameter(c, "@COD_CLIENTE", DbType.Int32, pk);
                result = InitDto(c, result);
            });

            return result;
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
            Helper.UsingCommand(Session.Connection, c => {
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
            throw new NotImplementedException();
        }

        public override Cliente InitDto(IDataRecord reader, Cliente dto)
        {
            dto.CodCliente = Helper.ReadInt32(reader, "COD_CLIENTE").Value;
            dto.Identificador = Helper.ReadInt32(reader, "IDENTIFICADOR").Value;
            dto.Cnpj = Helper.ReadString(reader, "CNPJ");
            dto.Nome = Helper.ReadString(reader, "NOME");
            dto.Senha = Helper.ReadString(reader, "SENHA");
            dto.CodEmpresa = Helper.ReadInt32(reader, "COD_EMPRESA").Value;
            dto.NomeEmpresa = Helper.ReadString(reader, "NOMEEMPRESA");
            dto.EmailNotificacao = Helper.ReadString(reader, "EMAILNOTIFICACAO");
            dto.ReceberNotificacao = Helper.ReadBoolean(reader, "RECEBERNOTIFICACAO").Value;

            return dto;
        }

        public virtual Cliente FindByPrimaryKey(int codCliente)
        {
            return FindByPrimaryKey((object) codCliente);
        }
    }
}