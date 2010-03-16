using System;
using System.Collections.Generic;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class ItemTransacaoDao : DataAccessBase<ItemTransacao>, IItemTransacaoDao
    {
        public override ItemTransacao InitDto(ItemTransacao dto)
        {
            dto.CodEmpresa = GenerateInt32();
            dto.CodTransacao = GenerateInt32();
            dto.CodItem = GenerateCode(10);
            dto.Quantidade = GenerateInt32(1, 999);
            dto.Descricao = GenerateName(4).ToUpper();

            return dto;
        }

        public IEnumerable<ItemTransacao> FindAll(int codEmpresa, int codTransacao)
        {
            return FindAll();
        }

        public override ItemTransacao FindByPrimaryKey(object pk)
        {
            throw new NotImplementedException();
        }

        public override ItemTransacao Insert(ItemTransacao dto)
        {
            return dto;
        }

        public IEnumerable<ItemTransacao> Insert(IEnumerable<ItemTransacao> dtos)
        {
            return dtos;
        }

        public override ItemTransacao Update(ItemTransacao dto)
        {
            throw new NotImplementedException();
        }
    }
}
