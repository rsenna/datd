using System;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class ServicoDao : DataAccessBase<Servico>, IServicoDao
    {
        public Servico[] FindAll(int codFamilia)
        {
            return FindAll();
        }

        public override Servico FindByPrimaryKey(object pk)
        {
            throw new NotImplementedException();
        }

        public override Servico Update(Servico dto)
        {
            throw new NotImplementedException();
        }

        public override Servico Insert(Servico dto)
        {
            throw new NotImplementedException();
        }

        public override Servico FetchDto()
        {
            return new Servico {
                CodItem = GenerateCode(10),
                Obrigatorio = GenerateBoolean(),
                Descricao = GenerateName(4),
                ServicoInterno = GenerateBoolean()
            };
        }
    }
}