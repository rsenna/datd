using System;
using System.Data;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    public class PacoteCreditoDao : DataAccessBase<PacoteCredito>, IPacoteCreditoDao
    {
        // TODO: Passar para stp
        private const string SQL_STMT_FIND_BY_PRIMARY_KEY = @"
            SELECT
                PACC.cod_pacote AS rcodpacote,
                PACC.quantidade AS rquantidadedisponivel,
                item.descricao AS rdescricao
            FROM
                pacote_cliente PACC
                INNER JOIN item ON
                    item.cod_item = PACC.cod_pacote
            WHERE
                PACC.cod_cliente = @PCOD_CLIENTE
                AND item.cod_item = @PCOD_PACOTE
        ";

        private const string SQL_STMT_FIND_BY_COD_CLIENTE = @"
            SELECT
                rcodpacote,
                rquantidadedisponivel,
                rdescricao
            FROM
                STP_WEBCONSULTARPACOTES(@PCOD_CLIENTE)
        ";

        public override PacoteCredito FetchDto(IDataRecord record)
        {
            var result = new PacoteCredito();

            result.CodPacoteCredito = Helper.ReadString(record, "RCODPACOTE");
            result.Quantidade = Helper.ReadDecimal(record, "RQUANTIDADEDISPONIVEL").Value;
            result.Descricao = Helper.ReadString(record, "RDESCRICAO");

            return result;
        }

        public PacoteCredito[] FindAll(int codCliente)
        {
            PacoteCredito[] result = null;

            Helper.UsingCommand(Session.Connection, c => {
                c.CommandText = SQL_STMT_FIND_BY_COD_CLIENTE;
                Helper.AddParameter(c, "@PCOD_CLIENTE", DbType.Int32, codCliente);
                result = FetchDtos(c);
            });

            return result;
        }

        public override PacoteCredito[] FindAll()
        {
            throw new NotImplementedException();
        }

        public override PacoteCredito FindByPrimaryKey(object pk)
        {
            throw new NotImplementedException();
        }

        public PacoteCredito FindByPrimaryKey(int codCliente, string codPacoteCliente)
        {
            PacoteCredito result = null;

            Helper.UsingCommand(Session.Connection, c => {
                c.CommandText = SQL_STMT_FIND_BY_PRIMARY_KEY;
                Helper.AddParameter(c, "@PCOD_CLIENTE", DbType.Int32, codCliente);
                Helper.AddParameter(c, "@PCOD_PACOTE", DbType.String, codPacoteCliente);
                result = FetchDto(c);
            });

            return result;
        }

        public override PacoteCredito Insert(PacoteCredito dto)
        {
            throw new NotImplementedException();
        }

        public override PacoteCredito Update(PacoteCredito dto)
        {
            throw new NotImplementedException();
        }
    }
}