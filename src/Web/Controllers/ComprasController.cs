using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web.Mvc;
using Dataweb.Dilab.Model.DataTransfer;
using Dataweb.Dilab.Model.Service;
using Dataweb.Dilab.Web.Models;

namespace Dataweb.Dilab.Web.Controllers
{
    public class ComprasController : ControllerBase
    {
        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index(string referencia)
        {
            var login = GetLogin();
            var transacoes = string.IsNullOrEmpty(referencia)?
                ProdutoSC.FindAllTransacaoByLogin(login) :
                ProdutoSC.FindAllTransacaoByLoginAndReferencia(login, referencia);

            var viewModel = new ComprasIndex {
                OrdensServico = transacoes.Where(item => item.Tipo == TipoTransacao.OrdemServico),
                Pedidos = transacoes.Where(item => item.Tipo == TipoTransacao.Pedido)
            };

            return View(viewModel);
        }

        private string GetDescricaoMaterial(OrdemServico os)
        {
            var materiais = ProdutoSC.FindAllMaterial();
            var material = materiais.First(m => m.CodMaterial == os.CodMaterial);
            return material == null? null : material.Descricao;
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Detalhar(int codEmpresa, int codTransacao, TipoTransacao tipo)
        {
            Transacao transacao;
            string descricaoMaterial = null;

            switch (tipo)
            {
                case TipoTransacao.OrdemServico:
                    transacao = ProdutoSC.GetOrdemServico(codEmpresa, codTransacao);
                    descricaoMaterial = GetDescricaoMaterial((OrdemServico) transacao);
                    break;

                case TipoTransacao.Pedido:
                    transacao = ProdutoSC.GetPedido(codEmpresa, codTransacao);
                    break;

                default:
                    transacao = ProdutoSC.GetTransacao(codEmpresa, codTransacao);
                    break;
            }

            var viewModel = new ComprasDetalhar {
                Transacao = transacao,
                Material = descricaoMaterial
            };

            return View(viewModel);
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult NovaOs()
        {
            var familias = ProdutoSC.FindAllFamilia();
            var materiais = ProdutoSC.FindAllMaterial();

            var viewModel = new ComprasNovaOs {
                Familias = familias,
                Materiais = materiais
            };

            return View(viewModel);
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult NovaOs(ComprasNovaOs viewModel)
        {
            var os = new OrdemServico {
                CodCliente = GetCodCliente(),
                Observacao = viewModel.ObservacaoGeral,
                Referencia = viewModel.Referencia,
                DescricaoArmacao = viewModel.Armacao,
                ObservacaoArmacao = viewModel.ObservacaoArmacao,
                CodMaterial = viewModel.MaterialArmacao,
                TipoVt = 1,
                Ta = viewModel.Ta,
                Md = viewModel.Md,
                Diametro = viewModel.Diametro,
                ObservacaoLente = string.Empty,
                Dp = viewModel.Dp,
                Aa = viewModel.Aa,
                Eixo = viewModel.Eixo,
                Ponte = viewModel.Ponte
            };

            var lenteOd = new OrdemServicoLente {
                TipoLente = TipoLente.OlhoDireito,
                CodFamilia = viewModel.FamiliaOd,
                Descricao = viewModel.DescricaoLenteOd,
                LongeEsf = viewModel.EsfLongeOd,
                LongeCil = viewModel.CilLongeOd,
                LongeEixo = viewModel.EixoLongeOd,
                Adicao = viewModel.AdicaoOd,
                PertoEsf = viewModel.EsfPertoOd,
                PertoCil = viewModel.CilPertoOd,
                PertoEixo = viewModel.EixoPertoOd,
                Dnp = viewModel.DnpOd,
                Alt = viewModel.AltOd
            };

            var lenteOe = new OrdemServicoLente {
                TipoLente = TipoLente.OlhoEsquerdo,
                CodFamilia = viewModel.FamiliaOe,
                Descricao = viewModel.DescricaoLenteOe,
                LongeEsf = viewModel.EsfLongeOe,
                LongeCil = viewModel.CilLongeOe,
                LongeEixo = viewModel.EixoLongeOe,
                Adicao = viewModel.AdicaoOe,
                PertoEsf = viewModel.EsfPertoOe,
                PertoCil = viewModel.CilPertoOe,
                PertoEixo = viewModel.EixoPertoOe,
                Dnp = viewModel.DnpOe,
                Alt = viewModel.AltOe
            };

            // Determina a quantidade dos serviços a partir das famílias selecionadas:
            var quantidade = lenteOd.CodFamilia == lenteOe.CodFamilia? 2 : 1;
            var itens = new ItemTransacao[viewModel.Servicos.Length];

            for (var i = 0; i < itens.Length; i++)
            {
                var servico = new ItemTransacao {
                    CodItem = viewModel.Servicos[i],
                    Quantidade = quantidade
                };

                itens[i] = servico;
            }

            os.LenteOd = lenteOd;
            os.LenteOe = lenteOe;
            os.Itens = itens;

            try
            {
                os = ProdutoSC.InsertOrdemServico(os);
            }
            catch (FaultException<DatawebFault> ex)
            {
                ModelState.AddModelError("ordemServicoMsg", ex.Detail.Message);
            }

            if (ModelState.IsValid)
            {
                return RedirectToAction("NovaOsSucesso", new {id = os.Numero});
            }

            return View();
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult NovaOsSucesso(string id)
        {
            ViewData["num"] = id;
            return View();
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult NovoPedido()
        {
            var model = new ComprasNovoPedido {
                Familias = ProdutoSC.FindAllFamilia()
            };

            return View(model);
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult NovoPedido(ComprasNovoPedido viewModel)
        {
            var pedido = new Pedido {
                CodCliente = GetCodCliente(),
                Observacao = viewModel.Observacao,
            };

            var produtos = new List<ItemTransacao>();

            // Obs: notar que laço começa em 1, não em 0. É necessário ignorar
            // a primeira linha da tabela, que serve como template para frontend.
            for (var i = 1; i < viewModel.Produtos.Length; i++)
            {
                var produto = new ItemTransacao {
                    CodItem = viewModel.Produtos[i],
                    Quantidade = Convert.ToInt32(viewModel.Quantidades[i])
                };

                produtos.Add(produto);
            }

            pedido.Itens = produtos.ToArray();

            try
            {
                pedido = ProdutoSC.InsertPedido(pedido);
            }
            catch (FaultException<DatawebFault> ex)
            {
                ModelState.AddModelError("pedidoMsg", ex.Detail.Message);
            }

            if (ModelState.IsValid)
            {
                return RedirectToAction("NovoPedidoSucesso", new {id = pedido.Numero});
            }

            return View();
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult NovoPedidoSucesso(string id)
        {
            ViewData["num"] = id;
            return View();
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ListarServicos(int familia)
        {
            var result = ProdutoSC.FindAllServico(familia);
            return new JsonResult {Data = result};
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ListarProdutos(int familia)
        {
            var result = ProdutoSC.FindAllProduto(familia);
            return new JsonResult {Data = result};
        }
    }
}