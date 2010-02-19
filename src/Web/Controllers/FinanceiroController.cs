using System.Web.Mvc;

namespace Dataweb.Dilab.Web.Controllers
{
    public class FinanceiroController : ControllerBase
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}