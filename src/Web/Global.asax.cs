using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Dataweb.Dilab.Web
{
    public class MvcApplication : HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Compras-Detalhar",
                "Compras/{codEmpresa}/{codTransacao}",
                new {controller = "Compras", action = "Detalhar"},
                new {codEmpresa = @"\d+", codTransacao = @"\d+"}
                );

            routes.MapRoute(
                "Compras-Referencia",
                "Compras/{referencia}",
                new {controller = "Compras", action = "Index"},
                new {referencia = @"\d+"}
                );

            routes.MapRoute(
                "Pacotes-Detalhar",
                "Pacotes/{codPacoteCliente}",
                new { controller = "Pacotes", action = "Detalhar" }
                );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new {controller = "Home", action = "Index", id = ""} // Parameter defaults
                );
        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
        }
    }
}