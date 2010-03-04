using System.Web.Mvc;
using Dataweb.Dilab.Web.Models;

namespace Dataweb.Dilab.Web.Controllers
{
    public class HomeController : ControllerBase
    {
        [Authorize]
        public ActionResult Index()
        {
            var login = GetLogin();
            var viewModel = new HomeViewModel {Cliente = ClienteSC.FindByLogin(login)};

            viewModel.EmProducao = ProdutoSC.GetCountEmProducao(viewModel.Cliente.CodCliente);
            viewModel.Fechadas = ProdutoSC.GetCountFechadas(viewModel.Cliente.CodCliente);

            return View(viewModel);
        }
    }
}