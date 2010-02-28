using System;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class ProdutoDao : DataAccessBase<Produto>, IProdutoDao
    {
        public Produto[] FindAll(int codFamilia)
        {
            return FindAll();
        }

        public override Produto FindByPrimaryKey(object pk)
        {
            throw new NotImplementedException();
        }

        public override Produto Update(Produto dto)
        {
            throw new NotImplementedException();
        }

        public override Produto Insert(Produto dto)
        {
            throw new NotImplementedException();
        }

        public override Produto FetchDto()
        {
            return new Produto {
                CodItem = GenerateCode(10),
                Obrigatorio = GenerateBoolean(),
                Descricao = GenerateName(4),
            };
        }
    }
}