using System;
using System.Collections.Generic;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class PedidoDao : DataAccessBase<Pedido>, IPedidoDao
    {
        private TransacaoDao compositionBaseDao;

        private TransacaoDao TransacaoDao
        {
            get
            {
                return compositionBaseDao ?? (compositionBaseDao = new TransacaoDao { Depth = GetDetailDepth() });
            }
        }

        public override Pedido InitDto(Pedido dto)
        {
            return (Pedido) TransacaoDao.InitDto(dto);
        }

        public override Pedido FindByPrimaryKey(object pk)
        {
            throw new NotImplementedException();
        }

        public override Pedido Update(Pedido dto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pedido> FindAll(int codCliente)
        {
            throw new NotImplementedException();
        }

        public Pedido FindByPrimaryKey(int codEmpresa, int codTransacao)
        {
            return InitDto(new Pedido());
        }

        public Pedido Close(Pedido dto)
        {
            return (Pedido) TransacaoDao.Close(dto);
        }

        public override Pedido Insert(Pedido dto)
        {
            dto.Numero = GenerateInt32();
            return dto;
        }
    }
}