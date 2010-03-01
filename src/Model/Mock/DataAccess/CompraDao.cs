using System;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class CompraDao : DataAccessBase<Compra>, ICompraDao
    {
        private const int MAX_FECHADAS = 20;
        private const int MAX_EM_PRODUCAO = 20;
        private const int MAX_DIAS = 20;

        public override Compra FetchDto()
        {
            return new Compra {
                CodEmpresa = GenerateInt32(),
                CodTransacao = GenerateInt32(),
                Numero = GenerateInt32(),
                Referencia = GenerateCode(7),
                Emissao = GenerateDateTime(-MAX_DIAS),
                Previsao = GenerateDateTime(MAX_DIAS),
                Expedicao = GenerateDateTime(MAX_DIAS),
                Etapa = (TipoEtapa)GenerateInt32(4),
                AvisoMensagem = GenerateParagraph(),
                Tipo = (TipoCompra)GenerateInt32(1, 2)
            };
        }

        public override Compra FindByPrimaryKey(object pk)
        {
            throw new NotImplementedException();
        }

        public override Compra Update(Compra dto)
        {
            throw new NotImplementedException();
        }

        public Compra[] FindAll(int codCliente)
        {
            return FindAll();
        }

        public int GetCountFechadas(int codCliente)
        {
            return GenerateInt32(MAX_FECHADAS);
        }

        public int GetCountEmProducao(int codCliente)
        {
            return GenerateInt32(MAX_EM_PRODUCAO);
        }

        public override Compra Insert(Compra dto)
        {
            throw new NotImplementedException();
        }
    }
}