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

        public ServicoOrdemServico[] InsertServicos(ServicoOrdemServico[] dtos)
        {
            return dtos;
        }
    }
}