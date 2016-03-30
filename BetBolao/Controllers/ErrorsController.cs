using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BetBolao.Controllers
{
    public class ErrorsController : Controller
    {
        public ActionResult Http404(Exception exception)
        {
            Response.StatusCode = 404;
            Response.ContentType = "text/html";
            return View("Error");
        }

        public ActionResult Http500(Exception exception)
        {
            var usu = new ContaController();
            System.Web.Security.FormsAuthentication.SignOut();
            Session.Abandon();
            Response.StatusCode = 500;
            Response.ContentType = "text/html";
            return View("ErroAcesso");
        }
    }
}