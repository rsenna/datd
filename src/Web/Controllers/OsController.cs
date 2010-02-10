using System.Web.Mvc;
using Dataweb.Dilab.Web.Model;

namespace Dataweb.Dilab.Web.Controllers
{
    [HandleError]
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
        public ActionResult Nova([Bind(Prefix = "")]OsNovaViewModel os)
        {
            return View(os);
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

        public ActionResult ListarServicos(int? familia)
        {
            InitWcf(); 

            var result = OrdemServicoSC.FindAllProdutoServico(familia, null);
            return new JsonResult {Data = result};
        }
    }
}