using System;
using System.Collections.Generic;
using System.Data;
using Dataweb.Dilab.Model.DataAccess.Contracts;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess
{
    public abstract class FamiliaDao : DataAccessBase<Familia>, IFamiliaDao
    {
        public abstract string GetStmtFindByPrimaryKey();
        public abstract string GetStmtFindAll();

        public override Familia InitDto(IReader reader, Familia dto)
        {
            dto.CodFamilia = reader.ReadRequired<int>("cod_produtofamilia");
            dto.Descricao = reader.ReadRequired("descricao");

            return dto;
        }

        public override Familia FindByPrimaryKey(object pk)
        {
            var result = new Familia();

            using (var c = Session.CreateCommand())
            {
                c.CommandText = GetStmtFindByPrimaryKey();
                c.AddParameter("@PCOD_PRODUTOFAMILIA", DbType.Int32, pk);
                result = InitDto(c, result);
            }

            return result;
        }

        public override Familia Update(Familia dto)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Familia> FindAll()
        {
            return FindAll(GetStmtFindAll());
        }

        public override Familia Insert(Familia dto)
        {
            throw new NotImplementedException();
        }
    }
}