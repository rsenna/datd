using System.Linq;
using System.Web.Mvc;
using Dataweb.Dilab.Model.DataTransfer;
using Dataweb.Dilab.Web.Models;

namespace Dataweb.Dilab.Web.Controllers
{
    public class FinanceiroController : ControllerBase
    {
        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index()
        {
            return RedirectToAction("Faturas");
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Faturas()
        {
            var login = GetLogin();
            var faturas = ProdutoSC.FindAllFatura(login);
            return View(faturas);
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult NotasFiscais()
        {
            var login = GetLogin();
            var nfs = ProdutoSC.FindAllNotaFiscal(login);
            return View(nfs);
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Lancamentos()
        {
            var login = GetLogin();
            var lancamentos = ProdutoSC.FindAllLancamento(login);
            return View(lancamentos);
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult DetalharFatura(int codFatura)
        {
            var fatura = ProdutoSC.GetFatura(codFatura);
            return View(fatura);
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult DetalharNotaFiscal(int codNotaFiscal)
        {
            var notaFiscal = ProdutoSC.GetNotaFiscal(codNotaFiscal);
            var viewModel = new FinanceiroDetalharNotaFiscal {
                NotaFiscal = notaFiscal,
                OrdensServico = notaFiscal.Transacoes.Where(item => item.Tipo == TipoTransacao.OrdemServico),
                Pedidos = notaFiscal.Transacoes.Where(item => item.Tipo == TipoTransacao.Pedido)
            };

            return View(viewModel);
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public string NotaFiscalEletronica(int codNotaFiscal)
        {
            Response.ContentType = "text/xml";
            Response.AddHeader("content-disposition", string.Format("attachment; filename=NFE{0:D}.xml", codNotaFiscal));
            var xml = ProdutoSC.GetXmlNotaFiscalEletronica(codNotaFiscal);
            return xml;
        }
    }
}