using System.ServiceModel;
using System.Web.Mvc;
using Dataweb.Dilab.Model.DataTransfer;
using Dataweb.Dilab.Model.Service;
using Dataweb.Dilab.Web.Model;

namespace Dataweb.Dilab.Web.Controllers
{
    public class OsController : ControllerBase
    {
        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index(string referencia)
        {
            InitWcf();

            var login = GetLogin();
            var items = string.IsNullOrEmpty(referencia)?
                OrdemServicoSC.FindAllByLogin(login) :
                OrdemServicoSC.FindAllByLoginAndReferencia(login, referencia);

            ViewData.Model = items;

            return View();
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Nova(OsNovaViewModel viewModel)
        {
            InitWcf();

            var os = new OrdemServico {
                CodCliente = GetCodCliente(),
                Observacao = viewModel.ObservacaoGeral,
                Referencia = viewModel.Referencia,
                DescricaoArmacao = string.Empty,
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

            var lenteOd = new OrdemServicoLente
            {
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
            var servicos = new ServicoOrdemServico[viewModel.Servicos.Length];

            for (var i = 0; i < servicos.Length; i++)
            {
                var servico = new ServicoOrdemServico {
                    CodItem = viewModel.Servicos[i],
                    Quantidade = quantidade
                };

                servicos[i] = servico;
            }

            var oso = new OrdemServicoOtica {
                OrdemServico = os,
                LenteOd = lenteOd,
                LenteOe = lenteOe,
                Servicos = servicos
            };

            try
            {
                OrdemServicoSC.InsertOrdemServico(oso);
            }
            catch (FaultException<DatawebFault> ex)
            {
                ModelState.AddModelError("ordemServicoMsg", ex.Detail.Message);
            }

            if (ModelState.IsValid)
            {
                return RedirectToAction("NovaSucesso");
            }

            return View();
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Nova()
        {
            InitWcf();

            var familias = OrdemServicoSC.FindAllFamilia();
            var materiais = OrdemServicoSC.FindAllMaterial();

            var viewModel = new OsNovaViewModel {
                Familias = familias,
                Materiais = materiais
            };

            return View(viewModel);
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult NovaSucesso()
        {
            return View();
        }

        public ActionResult ListarServicos(int familia)
        {
            InitWcf(); 

            var result = OrdemServicoSC.FindAllProdutoServico(familia);
            return new JsonResult {Data = result};
        }

    }
}