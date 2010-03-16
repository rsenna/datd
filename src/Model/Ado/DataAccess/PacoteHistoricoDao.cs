using System;
using System.Collections.Generic;
using System.Data;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Ado.DataAccess
{
    public class PacoteHistoricoDao : DataAccessBase<PacoteHistorico>, IPacoteHistoricoDao
    {
        private const string SQL_STMT_FIND_BY_COD_CLIENTE_AND_COD_PACOTE = @"
            SELECT
                RDATA,
                RNUMEROORDEMSERVICO,
                RQUANTIDADE,
                RTOTALCOMPRA,
                RTIPO
            FROM
                STP_WEBCONSULTARPACOTE(@PCODCLIENTE, @PCODPACOTE)
        ";

        public IEnumerable<PacoteHistorico> FindAll(int codCliente, string codPacoteCliente)
        {
            var result = new List<PacoteHistorico>();

            Helper.UsingCommand(Session.Connection, c => {
                c.CommandText = SQL_STMT_FIND_BY_COD_CLIENTE_AND_COD_PACOTE;
                Helper.AddParameter(c, "@PCODCLIENTE", DbType.Int32, codCliente);
                Helper.AddParameter(c, "@PCODPACOTE", DbType.String, codPacoteCliente);
                InitDtos(c, result);
            });

            return result;
        }

        public override PacoteHistorico InitDto(IDataRecord record, PacoteHistorico result)
        {
            result.Data = Helper.ReadDateTime(record, "RDATA").Value;
            result.NumeroOS = Helper.ReadInt32(record, "RNUMEROORDEMSERVICO");
            result.Quantidade = Helper.ReadDecimal(record, "RQUANTIDADE").Value;
            result.Valor = Helper.ReadDecimal(record, "RTOTALCOMPRA");
            result.Tipo = (TipoPacoteHistorico) Helper.ReadInt32(record, "RTIPO").Value;

            return result;
        }

        public override IEnumerable<PacoteHistorico> FindAll()
        {
            throw new NotImplementedException();
        }

        public override PacoteHistorico FindByPrimaryKey(object pk)
        {
            throw new NotImplementedException();
        }

        public override PacoteHistorico Insert(PacoteHistorico dto)
        {
            throw new NotImplementedException();
        }

        public override PacoteHistorico Update(PacoteHistorico dto)
        {
            throw new NotImplementedException();
        }
    }
}