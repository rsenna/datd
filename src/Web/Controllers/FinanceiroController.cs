using System.Web.Mvc;

namespace Dataweb.Dilab.Web.Controllers
{
    [HandleError]
    public class FinanceiroController : ControllerBase
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}