using System;
using System.Collections.Generic;
using System.Data;
using Dataweb.Dilab.Model.DataAccess.Contracts;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess
{
    public abstract class ClienteDao : DataAccessBase<Cliente>, IClienteDao
    {
        public abstract string GetStmtFindByIdentificador();
        public abstract string GetStmtFindByCnpj();
        public abstract string GetStmtFindByPrimaryKey();
        public abstract string GetStmtUpdate();

        public override Cliente InitDto(IReader reader, Cliente dto)
        {
            dto.CodCliente = reader.ReadRequired<int>("COD_CLIENTE");
            dto.Identificador = reader.ReadRequired<int>("IDENTIFICADOR");
            dto.Cnpj = reader.ReadOptional("CNPJ");
            dto.Nome = reader.ReadRequired("NOME");
            dto.Senha = reader.ReadRequired("SENHA");
            dto.CodEmpresa = reader.ReadRequired<int>("COD_EMPRESA");
            dto.NomeEmpresa = reader.ReadOptional("NOMEEMPRESA");
            dto.EmailNotificacao = reader.ReadOptional("EMAILNOTIFICACAO");
            dto.ReceberNotificacao = reader.ReadRequired<bool>("RECEBERNOTIFICACAO");

            return dto;
        }

        public Cliente FindByIdentificador(int identificador)
        {
            var result = new Cliente();

            using (var c = Session.CreateCommand())
            {
                c.CommandText = GetStmtFindByIdentificador();

                c.AddParameter("@IDENTIFICADOR", DbType.Int32, identificador);
                c.Prepare();

                result = InitDto(c, result);
            }

            return result;
        }

        public Cliente FindByCnpj(string cnpj)
        {
            var result = new Cliente();

            using (var c = Session.CreateCommand()) {
                c.CommandText = GetStmtFindByCnpj();

                c.AddParameter("@CNPJ", DbType.String, cnpj);

                result = InitDto(c, result);
            }

            return result;
        }

        public override IEnumerable<Cliente> FindAll()
        {
            throw new NotImplementedException();
        }

        public override Cliente FindByPrimaryKey(object pk)
        {
            var result = new Cliente();

            using (var c = Session.CreateCommand())
            {
                c.CommandText = GetStmtFindByPrimaryKey();
                c.AddParameter("@COD_CLIENTE", DbType.Int32, pk);
                result = InitDto(c, result);
            }

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
            using (var c = Session.CreateCommand())
            {
                c.CommandText = GetStmtUpdate();

                c.AddParameter("@EMAILNOTIFICACAO", DbType.String, dto.EmailNotificacao);
                c.AddParameter("@RECEBERNOTIFICACAO", DbType.String, dto.ReceberNotificacao);
                c.AddParameter("@SENHA", DbType.String, dto.Senha);
                c.AddParameter("@COD_CLIENTE", DbType.Int32, dto.CodCliente);

                c.Execute();
            }

            return dto;
        }

        public override Cliente Insert(Cliente dto)
        {
            throw new NotImplementedException();
        }

        public virtual Cliente FindByPrimaryKey(int codCliente)
        {
            return FindByPrimaryKey((object) codCliente);
        }
    }
}