using System;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class OrdemServicoDao : DataAccessBase<OrdemServico>, IOrdemServicoDao
    {
        public override OrdemServico FetchDto()
        {
            throw new NotImplementedException();
        }

        public OrdemServico[] FindAll(int codCliente)
        {
            throw new NotImplementedException();
        }

        public override OrdemServico Insert(OrdemServico dto)
        {
            var result = base.Insert(dto);

            result.CodEmpresa = GenerateInt32();
            result.CodOrdemServico = GenerateInt32();
            result.Numero = GenerateInt32();

            return result;
        }

        public ServicoOrdemServico[] InsertItens(ServicoOrdemServico[] dtos)
        {
            return dtos;
        }

        public OrdemServico Close(OrdemServico dto)
        {
            return dto;
        }
    }
}