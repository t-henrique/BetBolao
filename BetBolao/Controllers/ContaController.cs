using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BetBolao.Compartilhado.Models;

namespace BetBolao.Controllers
{
    public class ContaController : Controller
    {

        //código funcional
        //[HttpPost]
        //public ActionResult LogOn (FormCollection f, string returnUrl)
        //{
        //    var usuario = new Usuarios {Senha = f["senha"], Email = f["email"]};

        //    //usuario.LogarUsuario(usuario);
        //    if (usuario.LogarUsuario(usuario))
        //    {
        //        System.Web.Security.FormsAuthentication.SetAuthCookie(usuario.Email, false);
        //        return Redirect("~/Home/Index");
        //    }
        //    return View();
        //}


        #region get
        public ActionResult LogOn()
        {
            ViewBag.mensagem = this.Page_Load();
            return View(ViewBag);
        }
        
        public ActionResult LogOff()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            Session.Abandon();
            return Redirect("~/Home/Home");
        }
        #endregion

        #region post
        [HttpPost]
        public ActionResult LogOn(FormCollection f, string returnUrl)
        {
            var usuario = new Usuarios { Senha = f["senha"], Email = f["email"] };
            if (ModelState.IsValid)
            {
                //usuario.LogarUsuario(usuario);
                if (usuario.LogarUsuario(usuario))
                {
                    ManterLogIn(usuario);

                }
            }

            ModelState.AddModelError("", "O login ou senha estão incorretos, tente novamente!");
            return View();

        }
        #endregion

        #region protected

        protected string Page_Load()
        {
            //var lblSessionID = string.Format("Session id: {0}", Session.Count);
            var lblSessionID = string.Format("Session id: {0}", Session.SessionID);
            return lblSessionID;
        }

        protected void Armazenar(string conteudo)
        {
            Session["Conteudo"] = conteudo;
            ViewBag.Mensagem = "Conteúdo armazenado na sessão";

        }

        protected void Ler()
        {
            if (Session["Conteudo"] != null)
            {
                ViewBag.mensagem = String.Format("Conteúdo da Sessão: {0}", Session["Conteudo"].ToString());
            }
        }

        #endregion
        #region private
        private void ManterLogIn(Usuarios usuario)
        {

            ManterSessaoUsuario(usuario);

            FormsAuthentication.Initialize();
            var fat = new FormsAuthenticationTicket(1,
                usuario.Nome.ToString(),
                DateTime.Now,
                DateTime.Now.AddMinutes(14),
                false, "user", FormsAuthentication.FormsCookiePath);
            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName,
                FormsAuthentication.Encrypt(fat)));
            Response.Redirect(FormsAuthentication.GetRedirectUrl(usuario.Nome.ToString(), false));

        }

        private void ManterSessaoUsuario(Usuarios usuario)
        {

            Session["nome"] = usuario.Nome;
            Session["perfil"] = usuario.Perfil;
            Session["email"] = usuario.Email;
            Session["senha"] = usuario.Senha;
            Session["id"] = usuario.Id;
            Session["dataNascimento"] = usuario.DataNascimento;
            Session["comentarios"] = usuario.Comentarios;
            Session["timeFavorito"] = usuario.TimeFavorito;
        }

#endregion
    }
}
