using System.Web.Mvc;

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
            return View();
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult NotasFiscais()
        {
            return View();
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Lancamentos()
        {
            return View();
        }
    }
}