using System;
using System.Collections.Generic;
using System.Data;
using Dataweb.Dilab.Model.DataAccess.Contracts;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess
{
    public abstract class PacoteHistoricoDao : DataAccessBase<PacoteHistorico>, IPacoteHistoricoDao
    {
        public abstract string GetStmtFindByCodClienteAndCodPacote();

        public override PacoteHistorico InitDto(IReader reader, PacoteHistorico result)
        {
            result.Data = reader.ReadRequired<DateTime>("RDATA");
            result.NumeroOS = reader.ReadOptional<int>("RNUMEROORDEMSERVICO");
            result.Quantidade = reader.ReadRequired<decimal>("RQUANTIDADE");
            result.Valor = reader.ReadOptional<decimal>("RTOTALCOMPRA");
            result.Tipo = reader.ReadRequired<TipoPacoteHistorico>("RTIPO");

            return result;
        }

        public IEnumerable<PacoteHistorico> FindAll(int codCliente, string codPacoteCliente)
        {
            var result = new List<PacoteHistorico>();

            using (var c = Session.CreateCommand())
            {
                c.CommandText = GetStmtFindByCodClienteAndCodPacote();
                c.AddParameter("@PCODCLIENTE", DbType.Int32, codCliente);
                c.AddParameter("@PCODPACOTE", DbType.String, codPacoteCliente);
                InitDtos(c, result);
            }

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