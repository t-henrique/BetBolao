using System.Web.Mvc;

namespace BetBolao.Controllers
{
    
    public class HomeController : Controller
    {

        //System.Web.HttpContext.Current.Session["sessionString"] = sessionValue; 
        //// GET: Home
        //public ActionResult Index()
        //{
        //    return View();
        //}
        [AllowAnonymous]
        public ActionResult Home()
        {
            ViewBag.mensagem = this.Page_Load();
            ViewBag.nome = Session["Nome"] as string;
            return View(ViewBag);

        }

        protected string Page_Load()
        {
            var lblSessionID = string.Format(": {0}", Session["nome"]);
            //var lblSessionID = string.Format("Session id: {0}", Session.SessionID);
            return lblSessionID;
        }
        [Authorize]
        public ActionResult HomeAdministrativa()
        {
            return View();
        }

        //public ActionResult HomeUsuario()
        //{
        //    return View();
        //}

        [Authorize]
        public ActionResult HomeUsuario()
        {
            return View();
        }

        public ActionResult Login()
        {
            return ContextDependentView();
        }

        private ActionResult ContextDependentView()
        {
            string actionName = ControllerContext.RouteData.GetRequiredString("action");
            if (Request.QueryString["content"] != null)
            {
                ViewBag.FormAction = "Json" + actionName;
                return PartialView();
            }
            ViewBag.FormAction = actionName;
            return View();
        }
    }
}