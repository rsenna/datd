using System;
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

        public PacoteHistorico[] FindAll(int codCliente, string codPacoteCliente)
        {
            PacoteHistorico[] result = null;

            Helper.UsingCommand(c =>
            {
                c.CommandText = SQL_STMT_FIND_BY_COD_CLIENTE_AND_COD_PACOTE;
                Helper.AddParameter(c, "@PCODCLIENTE", DbType.Int32, codCliente);
                Helper.AddParameter(c, "@PCODPACOTE", DbType.String, codPacoteCliente);
                result = FetchDtos(c);
            });

            return result;
        }

        public override PacoteHistorico FetchDto(IDataRecord record)
        {
            var result = new PacoteHistorico();

            result.Data = Helper.ReadDateTime(record, "RDATA").Value;
            result.NumeroOS = Helper.ReadInt32(record, "RNUMEROORDEMSERVICO");
            result.Quantidade = Helper.ReadDecimal(record, "RQUANTIDADE").Value;
            result.Valor = Helper.ReadDecimal(record, "RTOTALCOMPRA");
            result.Tipo = (TipoPacoteHistorico) Helper.ReadInt32(record, "RTIPO").Value;

            return result;
        }

        public override PacoteHistorico[] FindAll()
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