using System;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class PedidoDao : DataAccessBase<Pedido>, IPedidoDao
    {
        public override Pedido FetchDto()
        {
            throw new NotImplementedException();
        }

        public Pedido[] FindAll(int codCliente)
        {
            throw new NotImplementedException();
        }

        public override Pedido Insert(Pedido dto)
        {
            var result = base.Insert(dto);

            result.CodEmpresa = GenerateInt32();
            result.CodPedido = GenerateInt32();
            result.Numero = GenerateInt32();

            return result;
        }

        public ProdutoPedido[] InsertProdutos(ProdutoPedido[] dtos)
        {
            return dtos;
        }
    }
}