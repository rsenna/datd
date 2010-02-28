using System.Web.Mvc;
using Dataweb.Dilab.Web.Models;

namespace Dataweb.Dilab.Web.Controllers
{
    public class HomeController : ControllerBase
    {
        [Authorize]
        public ActionResult Index()
        {
            InitWcf();

            var login = GetLogin();
            var viewModel = new HomeViewModel {Cliente = ClienteSC.FindByLogin(login)};

            viewModel.EmProducao = OrdemServicoSC.GetCountEmProducao(viewModel.Cliente.CodCliente);
            viewModel.Fechadas = OrdemServicoSC.GetCountFechadas(viewModel.Cliente.CodCliente);

            return View(viewModel);
        }
    }
}