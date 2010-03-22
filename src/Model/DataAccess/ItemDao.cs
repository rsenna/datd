using System;
using System.Collections.Generic;
using System.Data;
using Dataweb.Dilab.Model.DataAccess.Contracts;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.DataAccess
{
    public abstract class ItemDao : DataAccessBase<Item>, IItemDao
    {
        public abstract string GetStmtFindAllProdutoByCodFamilia();
        public abstract string GetStmtFindAllServicoByCodFamilia();

        public override Item InitDto(IReader reader, Item result)
        {
            result.CodItem = reader.ReadRequired("cod_item");
            result.CodBarra = reader.ReadOptional("codigobarra");
            result.Obrigatorio = reader.ReadRequired<bool>("obrigatorio");
            result.Descricao = reader.ReadRequired("descricao");
            result.ServicoInterno = reader.ReadRequired<bool>("servicointerno");
            result.Tipo = reader.ReadRequired<TipoItem>("tipo");

            return result;
        }

        public override IEnumerable<Item> FindAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> FindAll(int codFamilia, TipoItem tipo)
        {
            var result = new List<Item>();
            var proc = tipo == TipoItem.Produto ? GetStmtFindAllProdutoByCodFamilia() : GetStmtFindAllServicoByCodFamilia();

            using (var c = Session.CreateCommand())
            {
                c.CommandText = proc;
                c.AddParameter("@COD_PRODUTOFAMILIA", DbType.Int32, codFamilia);
                InitDtos(c, result);
            }

            return result;
        }

        public override Item FindByPrimaryKey(object pk)
        {
            throw new NotImplementedException();
        }

        public override Item Update(Item dto)
        {
            throw new NotImplementedException();
        }

        public override Item Insert(Item dto)
        {
            throw new NotImplementedException();
        }
    }
}