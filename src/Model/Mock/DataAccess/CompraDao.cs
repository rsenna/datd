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
            Compra result = null;
            var tipo = (TipoCompra) GenerateInt32(1, 2);

            switch (tipo)
            {
                case TipoCompra.OrdemServico:
                    result = new OrdemServico();
                    break;

                case TipoCompra.Pedido:
                    result = new Pedido();
                    break;
            }

            if (result == null) throw new InvalidOperationException();

            result.Tipo = tipo;

            result.CodEmpresa = GenerateInt32();
            result.CodTransacao = GenerateInt32();
            result.Numero = GenerateInt32();
            result.Referencia = GenerateCode(7);
            result.Emissao = GenerateDateTime(-MAX_DIAS);
            result.Previsao = GenerateDateTime(MAX_DIAS);
            result.Expedicao = GenerateDateTime(MAX_DIAS);
            result.Etapa = (TipoEtapa) GenerateInt32(4);
            result.AvisoMensagem = GenerateParagraph();

            if (result is OrdemServico)
            {
                var os = (OrdemServico) result;

                os.DescricaoArmacao = GenerateParagraph();
                os.ObservacaoArmacao = GenerateParagraph();
                os.CodMaterial = GenerateInt32();
                os.TipoVt = GenerateInt32();
                os.Ta = GenerateDecimal();
                os.Md = GenerateDecimal();
                os.Diametro = GenerateDecimal();
                os.ObservacaoLente = GenerateParagraph();
                os.Dp = GenerateDecimal();
                os.Aa = GenerateDecimal();
                os.Eixo = GenerateDecimal();
                os.Ponte = GenerateDecimal();
            }

            return result;
        }

        public override Compra FindByPrimaryKey(object pk)
        {
            throw new NotImplementedException();
        }

        public override Compra Update(Compra dto)
        {
            throw new NotImplementedException();
        }

        public Compra[] FindAll(int codCliente, ProfundidadeConsultaTransacao profundidade)
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

        public Compra Close(Compra dto)
        {
            return dto;
        }

        public override Compra Insert(Compra dto)
        {
            dto.Numero = GenerateInt32();
            return dto;
        }
    }
}