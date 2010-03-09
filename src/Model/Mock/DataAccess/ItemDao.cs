using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class ItemDao : DataAccessBase<Item>, IItemDao
    {
        public Item[] FindAll(int codFamilia)
        {
            return FindAll();
        }

        public override Item FindByPrimaryKey(object pk)
        {
            throw new NotImplementedException();
        }

        public override Item Update(Item dto)
        {
            throw new NotImplementedException();
        }

        public Item[] FindAll(int codFamilia, TipoItem tipo)
        {
            return FindAll();
        }

        public override Item Insert(Item dto)
        {
            throw new NotImplementedException();
        }

        public override Item FetchDto()
        {
            return new Item
            {
                CodItem = GenerateCode(10),
                CodBarra = GenerateCode(10),
                Descricao = GenerateName(4),
                Obrigatorio = GenerateBoolean(),
                ServicoInterno = GenerateBoolean(),
                Tipo = (TipoItem) GenerateInt32(1, 2)
            };
        }
    }
}
