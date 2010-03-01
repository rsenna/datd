using System;
using System.Data;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    public class PedidoDao : DataAccessBase<Pedido>, IPedidoDao
    {
        private const string SQL_STMT_INSERT = @"
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

        private const string SQL_STMT_INSERT_ITEM = @"
            EXECUTE PROCEDURE STP_WEBORDEMSERVICO_ADDITEM(
                @PCOD_EMPRESA,
                @PCOD_ORDEMSERVICO,
                @PCOD_ITEM,
                @PQUANTIDADE
            )";

        private const string SQL_STMT_CLOSE = @"
            EXECUTE PROCEDURE STP_WEBORDEMSERVICO_FECHAR(
                @PCOD_EMPRESA,
                @PCOD_ORDEMSERVICO
            )";

        public override Pedido FetchDto(IDataRecord record)
        {
            throw new NotImplementedException();
        }

        public override Pedido[] FindAll()
        {
            throw new NotImplementedException();
        }

        public Pedido[] FindAll(int codCliente)
        {
            throw new NotImplementedException();
        }

        public override Pedido FindByPrimaryKey(object pk)
        {
            throw new NotImplementedException();
        }

        public override Pedido Insert(Pedido dto)
        {
            Helper.UsingCommand(c =>
            {
                c.CommandText = SQL_STMT_INSERT;

                // Entrada:
                Helper.AddParameter(c, "@PCOD_CLIENTE", DbType.Int32, dto.CodCliente);
                Helper.AddParameter(c, "@POBSERVACAO", DbType.String, dto.Observacao);

                // Parâmetros abaixo só são utilizados para OS, portanto vão em branco:
                Helper.AddParameter(c, "@PREFERENCIA", DbType.String, DBNull.Value);
                Helper.AddParameter(c, "@PDESCRICAOARMACAO", DbType.String, DBNull.Value);
                Helper.AddParameter(c, "@POBSERVACAOARMACAO", DbType.String, DBNull.Value);
                Helper.AddParameter(c, "@PCOD_OTICALENTEMATERIAL", DbType.Int32, DBNull.Value);
                Helper.AddParameter(c, "@PTIPOVT", DbType.Int32, 0);
                Helper.AddParameter(c, "@PTA", DbType.Decimal, DBNull.Value);
                Helper.AddParameter(c, "@PMD", DbType.Decimal, DBNull.Value);
                Helper.AddParameter(c, "@PDIAMETRO", DbType.Decimal, DBNull.Value);
                Helper.AddParameter(c, "@POBSERVACAOLENTE", DbType.String, DBNull.Value);
                Helper.AddParameter(c, "@PDP", DbType.Decimal, DBNull.Value);
                Helper.AddParameter(c, "@PAA", DbType.Decimal, DBNull.Value);
                Helper.AddParameter(c, "@PEIXO", DbType.Decimal, DBNull.Value);
                Helper.AddParameter(c, "@PPONTE", DbType.Decimal, DBNull.Value);

                // Saída:
                var paramCodEmpresa = Helper.AddReturnParameter(c, "@RCOD_EMPRESA", DbType.Int32);
                var paramCodOs = Helper.AddReturnParameter(c, "@RCOD_ORDEMSERVICO", DbType.Int32);
                var paramNumero = Helper.AddReturnParameter(c, "@RNUMEROORDEMSERVICO", DbType.Int32);

                c.ExecuteNonQuery();

                dto.CodEmpresa = (int) paramCodEmpresa.Value;
                dto.CodPedido = (int) paramCodOs.Value;
                dto.Numero = (int) paramNumero.Value;
            });

            return dto;
        }

        public virtual ProdutoPedido[] InsertItens(ProdutoPedido[] dtos)
        {
            // Utilizando uma transação única; ou gravo todos os serviços, ou não gravo nenhum.
            Helper.UsingTransaction(t =>
            {
                foreach (var dto in dtos)
                {
                    var item = dto; // P/ evitar warning; objeto será acessado dentro do delegate.

                    Helper.UsingCommand(t, c =>
                    {
                        c.CommandText = SQL_STMT_INSERT_ITEM;

                        Helper.AddParameter(c, "@PCOD_EMPRESA", DbType.Int32, item.CodEmpresa);
                        Helper.AddParameter(c, "@PCOD_ORDEMSERVICO", DbType.Int32, item.CodPedido);
                        Helper.AddParameter(c, "@PCOD_ITEM", DbType.String, item.CodItem);
                        Helper.AddParameter(c, "@PQUANTIDADE", DbType.Decimal, item.Quantidade);

                        c.ExecuteNonQuery();
                    });
                }
            });

            return dtos; // Deveriam ser atualizados a partir dos registros completos na base.
        }

        public override Pedido Update(Pedido dto)
        {
            throw new NotImplementedException();
        }

        public Pedido Close(Pedido dto)
        {
            Helper.UsingCommand(c =>
            {
                c.CommandText = SQL_STMT_CLOSE;

                // Entrada:
                Helper.AddParameter(c, "@PCOD_EMPRESA", DbType.Int32, dto.CodCliente);
                Helper.AddParameter(c, "@PCOD_ORDEMSERVICO", DbType.Int32, dto.CodPedido);

                c.ExecuteNonQuery();
            });

            return dto;
        }
    }
}