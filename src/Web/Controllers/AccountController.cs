using System;
using System.ServiceModel;
using System.Web.Mvc;
using System.Web.Security;
using Dataweb.Dilab.Model.Wcf;

namespace Dataweb.Dilab.Web.Controllers
{
    [HandleError]
    public class AccountController : ControllerBase
    {
        private const int PASSWORD_LENGTH = 5;

        public ActionResult LogOn()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ContentResult Verify(string userId, string password)
        {
            InitWcf();
            var result = "0";

            try
            {
                if (ClienteSC.ValidateLogin(userId, password))
                {
                    result = "1";
                }
            }
            catch(Exception)
            {
                result = "-1";
            }

            return new ContentResult { Content = result };
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult LogOn(string login, string password, bool rememberMe, string returnUrl)
        {
            InitWcf();

            if (!ClienteSC.ValidateLogin(login, password))
            {
                return View();
            }

            FormsAuthentication.SetAuthCookie(login, rememberMe);

            if (!String.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult MinhaConta()
        {
            InitWcf();

            var cliente = ClienteSC.FindByLogin(GetLogin());

            ViewData["emailNotificacao"] = cliente.EmailNotificacao;
            ViewData["receberNotificacao"] = cliente.ReceberNotificacao;
            ViewData["passwordLength"] = PASSWORD_LENGTH;
            
            return View();
        }

        private static bool SenhasEmBranco(params string[] senhas)
        {
            foreach (var senha in senhas)
            {
                if (!string.IsNullOrEmpty(senha))
                {
                    return false;
                }
            }

            return true;
        }
        
        private static bool TamanhoSenhaValido(params string[] senhas)
        {
            foreach (var senha in senhas)
            {
                if (string.IsNullOrEmpty(senha) || senha.Length < PASSWORD_LENGTH)
                {
                    return false;
                }
            }

            return true;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult MinhaConta(string senhaAtual, string senhaNova, string senhaConfirmacao, string emailNotificacao, bool receberNotificacao)
        {
            InitWcf();

            var login = GetLogin();
            var email = false;
            var senha = false;

            if (!string.IsNullOrEmpty(emailNotificacao))
            {
                email = true;

                if (!ValidateEmail(emailNotificacao))
                {
                    ModelState.AddModelError("emailNotificacao", "Para alterar o endereço de email, utilize um endereço válido.");
                }

            }

            if (!SenhasEmBranco(senhaAtual, senhaNova, senhaConfirmacao))
            {
                senha = true;

                if (!TamanhoSenhaValido(senhaNova, senhaConfirmacao))
                {
                    ModelState.AddModelError("senhaAtual", "Verifique o tamanho das senhas.");
                }

                if (!ClienteSC.ValidateLogin(login, senhaAtual))
                {
                    ModelState.AddModelError("senhaAtual", "Senha atual inválida.");
                }

                if (senhaNova != senhaConfirmacao)
                {
                    ModelState.AddModelError("senhaConfirmacao", "Confirmação de senha não corresponde à senha indicada.");
                }

            }

            if (email && ModelState.IsValid)
            {
                try
                {
                    ClienteSC.ChangeEmail(login, emailNotificacao, receberNotificacao);
                }
                catch (FaultException<DatawebFault> ex)
                {
                    ModelState.AddModelError("emailNotificacao", ex.Detail.Message);
                }
            }

            if (senha && ModelState.IsValid)
            {
                try
                {
                    ClienteSC.ChangePassword(login, senhaAtual, senhaNova);
                }
                catch (FaultException<DatawebFault> ex)
                {
                    ModelState.AddModelError("senhaAtual", ex.Detail.Message);
                }
            }

            if (ModelState.IsValid)
            {
                return RedirectToAction("MinhaContaSucesso");
            }

            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult MinhaContaSucesso()
        {
            return View();
        }
    }
}