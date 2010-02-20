using System;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class ProdutoServicoDao : DataAccessBase<ProdutoServico>, IProdutoServicoDao
    {
        public ProdutoServico[] FindAll(int codFamilia)
        {
            return FindAll();
        }

        public override ProdutoServico FindByPrimaryKey(object pk)
        {
            throw new NotImplementedException();
        }

        public override ProdutoServico Update(ProdutoServico dto)
        {
            throw new NotImplementedException();
        }

        public override ProdutoServico Insert(ProdutoServico dto)
        {
            throw new NotImplementedException();
        }

        public override ProdutoServico FetchDto()
        {
            return new ProdutoServico {
                CodItem = GenerateCode(10),
                Obrigatorio = GenerateBoolean(),
                Descricao = GenerateName(4),
                ServicoInterno = GenerateBoolean()
            };
        }
    }
}