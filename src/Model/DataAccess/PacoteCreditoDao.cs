using System;
using System.Collections.Generic;
using System.Data;
using Dataweb.Dilab.Model.DataAccess.Contracts;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess
{
    public abstract class PacoteCreditoDao : DataAccessBase<PacoteCredito>, IPacoteCreditoDao
    {
        public abstract string GetStmtFindByCodCliente();
        public abstract string GetStmtFindByPrimaryKey();

        public override PacoteCredito InitDto(IReader reader, PacoteCredito result)
        {
            result.CodPacoteCredito = reader.ReadRequired("RCODPACOTE");
            result.Quantidade = reader.ReadRequired<decimal>("RQUANTIDADEDISPONIVEL");
            result.Descricao = reader.ReadRequired("RDESCRICAO");

            return result;
        }

        public IEnumerable<PacoteCredito> FindAll(int codCliente)
        {
            var result = new List<PacoteCredito>();

            using (var c = Session.CreateCommand())
            {
                c.CommandText = GetStmtFindByCodCliente();
                c.AddParameter("@PCOD_CLIENTE", DbType.Int32, codCliente);
                InitDtos(c, result);
            }

            return result;
        }

        public override IEnumerable<PacoteCredito> FindAll()
        {
            throw new NotImplementedException();
        }

        public override PacoteCredito FindByPrimaryKey(object pk)
        {
            throw new NotImplementedException();
        }

        public PacoteCredito FindByPrimaryKey(int codCliente, string codPacoteCliente)
        {
            var result = new PacoteCredito();

            using (var c = Session.CreateCommand())
            {
                c.CommandText = GetStmtFindByPrimaryKey();
                c.AddParameter("@PCOD_CLIENTE", DbType.Int32, codCliente);
                c.AddParameter("@PCOD_PACOTE", DbType.String, codPacoteCliente);
                result = InitDto(c, result);
            }

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