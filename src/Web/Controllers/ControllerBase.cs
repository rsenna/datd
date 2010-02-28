using System;
using System.Configuration;
using System.Globalization;
using System.ServiceModel;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;
using Dataweb.Dilab.Web.ClienteServiceReference;
using Dataweb.Dilab.Web.Configuration;
using Dataweb.Dilab.Web.Models;
using Dataweb.Dilab.Web.OrdemServicoServiceReference;
using Elmah;

namespace Dataweb.Dilab.Web.Controllers
{
    public abstract class ControllerBase : Controller, IHasTenant
    {
        private const string CULTURE = "pt-BR";

        protected ClienteServiceClient ClienteSC { get; set; }
        protected OrdemServicoServiceClient OrdemServicoSC { get; set; }
        public Tenant Tenant { get; set; }

        protected void InitWcf()
        {
            if (Tenant == null)
            {
                return;
            }

            ClienteSC = CreateServiceClient<ClienteServiceClient, IClienteService>();
            OrdemServicoSC = CreateServiceClient<OrdemServicoServiceClient, IOrdemServicoService>();
        }

        protected override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            if (context.ExceptionHandled)
            {
                return;
            }

            // Avoid to show YSOD to end-users
            if (context.HttpContext.IsCustomErrorEnabled)
            {
                var controllerName = context.RouteData.GetRequiredString("controller");
                var actionName = context.RouteData.GetRequiredString("action");
                var httpContext = System.Web.HttpContext.Current;

                context.ExceptionHandled = true;

                ErrorSignal.FromContext(httpContext).Raise(context.Exception, httpContext);

                View("Error", new HandleErrorInfo(context.Exception, controllerName, actionName)).ExecuteResult(ControllerContext);
            }
        }

        /// <summary>
        /// Instancia um novo service-client, considerando que a sua url será
        /// a combinação da url básica, definida pelo tenant e via Web.Config,
        /// e url relativa do serviço desejado
        /// </summary>
        /// <typeparam name="T">Tipo do cliente</typeparam>
        /// <typeparam name="TInterface">Tipo da interface implementada pelo
        /// serviço</typeparam>
        /// <returns>Nova instancia do cliente</returns>
        /// <remarks><typeparamref name="TInterface"/> é necessário apenas
        /// para satisfazer a sintaxe do C#, e não é utilizado diretamente no
        /// código</remarks>
        private T CreateServiceClient<T, TInterface>()
            where TInterface : class
            where T : ClientBase<TInterface>, new()
        {
            var clientBase = new T();

            var baseUri = new Uri(Tenant.Url);
            var relativeUri = clientBase.Endpoint.Address.Uri.AbsolutePath;
            var endpointUri = new Uri(baseUri, relativeUri);

            clientBase.Endpoint.Address = new EndpointAddress(endpointUri);

            return clientBase;
        }

        protected virtual string GetLogin()
        {
            var userIdentity = System.Web.HttpContext.Current.User.Identity;
            return userIdentity.Name;
        }

        protected virtual int GetCodCliente()
        {
            // TODO: poderia utilizar sessao; metodo foi criado com este objetivo.
            var cliente = ClienteSC.FindByLogin(GetLogin());
            return cliente.CodCliente;
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            var ci = CultureInfo.GetCultureInfo(CULTURE);

            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            var dilabConfiguration = ConfigurationManager.GetSection("Dilab") as DilabSection;

            if (dilabConfiguration == null)
            {
                throw new ConfigurationErrorsException("The <Dilab> section is not defined in configuration file.");
            }

            var tenantCollection = dilabConfiguration.Tenants;

            if (tenantCollection == null)
            {
                throw new ConfigurationErrorsException("The <Tenants> section is not defined in configuration file.");
            }

            var hostParts = requestContext.HttpContext.Request.Headers["Host"].Split('.');
            var tenantName = (hostParts.Length) > 1? hostParts[0] : "";

            Tenant = null;

            foreach (TenantElement t in tenantCollection)
            {
                if (String.Equals(t.Name, tenantName, StringComparison.InvariantCultureIgnoreCase))
                {
                    Tenant = new Tenant {Name = t.Name, Url = t.Url};
                }
            }

            ViewData["Tenant"] = Tenant;
            ViewData["Tenant.Name"] = (Tenant != null)? Tenant.Name : "";

            base.Initialize(requestContext);
        }

        // TODO: implementar validação de email
        public bool ValidateEmail(string email)
        {
            return true;
        }
    }
}