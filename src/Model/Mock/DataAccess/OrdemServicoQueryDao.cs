using System;
using Dataweb.Dilab.Model.DataAccess;
using Dataweb.Dilab.Model.DataTransfer;

namespace Dataweb.Dilab.Model.Mock.DataAccess
{
    public class OrdemServicoQueryDao : DataAccessBase<OrdemServicoQuery>, IOrdemServicoQueryDao
    {
        private const int MAX_FECHADAS = 20;
        private const int MAX_EM_PRODUCAO = 20;
        private const int MAX_DIAS = 20;

        public override OrdemServicoQuery FetchDto()
        {
            return new OrdemServicoQuery {
                CodEmpresa = GenerateInt32(),
                CodTransacao = GenerateInt32(),
                NumeroOrdemServico = GenerateInt32(),
                Referencia = GenerateCode(7),
                Emissao = GenerateDateTime(-MAX_DIAS),
                Previsao = GenerateDateTime(MAX_DIAS),
                Expedicao = GenerateDateTime(MAX_DIAS),
                Etapa = (TipoEtapa)GenerateInt32(4),
                AvisoMensagem = GenerateParagraph()
            };
        }

        public override OrdemServicoQuery FindByPrimaryKey(object pk)
        {
            throw new NotImplementedException();
        }

        public override OrdemServicoQuery Update(OrdemServicoQuery dto)
        {
            throw new NotImplementedException();
        }

        public OrdemServicoQuery[] FindAll(int codCliente)
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

        public override OrdemServicoQuery Insert(OrdemServicoQuery dto)
        {
            throw new NotImplementedException();
        }
    }
}