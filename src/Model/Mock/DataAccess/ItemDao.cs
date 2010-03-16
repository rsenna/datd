using System;
using System.Collections.Generic;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class ItemDao : DataAccessBase<Item>, IItemDao
    {
        public IEnumerable<Item> FindAll(int codFamilia)
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

        public IEnumerable<Item> FindAll(int codFamilia, TipoItem tipo)
        {
            return FindAll();
        }

        public override Item Insert(Item dto)
        {
            throw new NotImplementedException();
        }

        public override Item InitDto(Item dto)
        {
            dto.CodItem = GenerateCode(10);
            dto.CodBarra = GenerateCode(10);
            dto.Descricao = GenerateName(4).ToUpper();
            dto.Obrigatorio = GenerateBoolean();
            dto.ServicoInterno = GenerateBoolean();
            dto.Tipo = (TipoItem) GenerateInt32(1, 2);

            return dto;
        }
    }
}
