using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BetBolao.Compartilhado.Dao;
using BetBolao.Models;

namespace BetBolao.Controllers
{
    [Authorize]
    public class TimesController : Controller
    {
        #region Private Variables
        private static string _resultado;
        private DaoTimes _dao = new DaoTimes();
        private List<Pais> _paises = new List<Pais>();
        private List<Time> _times = new List<Time>();
        private Time _time = new Time();

        #endregion


        // GET: Times
        #region Métodos GET Public
        public ActionResult Index()
        {
            var idusuario = (int)Session["id"];
            var auth = new AutenticacaoSeguranca();
            if (!auth.PermissaoAcessoUsuarioApostador(idusuario))
            {
                return RedirectToAction("ListarTodosOsTimes");
            }
            return View("_NaoAutorizado");
        }

        public ActionResult CadastrarTime()
        {var idusuario = (int)Session["id"];
            var auth = new AutenticacaoSeguranca();
            if (!auth.PermissaoAcessoUsuarioApostador(idusuario))
            {
                ViewBag.IdPais = CarregaTodosPaises();
                if (TempData["mensagem"] is Nullable)
                {
                    var mensagem = TempData["mensagem"] as string;
                }

                return View();
            }
            return View("_NaoAutorizado");
        }


        public ActionResult ListarTodosOsTimes()
        {var idusuario = (int)Session["id"];
            var auth = new AutenticacaoSeguranca();
            if (!auth.PermissaoAcessoUsuarioApostador(idusuario))
            {
                if (TempData["mensagem"] is Nullable)
                {
                    var mensagem = TempData["mensagem"] as string;
                }
                var time = new Time();
                time.RetornaTimes(_times);
                return View(_times);
            }
            return View("_NaoAutorizado");
        }

        public ActionResult EditarTime(int id)
        {var idusuario = (int)Session["id"];
            var auth = new AutenticacaoSeguranca();
            if (!auth.PermissaoAcessoUsuarioApostador(idusuario))
            {
                var time = new Time();
                return View(time.RetornaTime(id));
            }
            return View("_NaoAutorizado");
        }

        #endregion


        #region Médotos Post Public
        [HttpPost]
        public ActionResult CadastrarTime(Time time)
        {
            if (ModelState.IsValid)
            {
                string resultado = _dao.CadastrarTime(time);
                TimesController._resultado = resultado;
                TempData["mensagem"] = TimesController._resultado;
                return RedirectToAction("CadastrarTime");
            }
            return View();
        }
        [HttpPost]
        public ActionResult EditarTime(Time time)
        {
            if (ModelState.IsValid)
            {
                TempData["mensagem"] = _dao.AtualizarTime(time);
            }
            return RedirectToAction("ListarTodosOsTimes");
        }
        #endregion

        #region Métodos Privados
        private SelectList CarregaTodosPaises()
        {
            return new SelectList(
                      new Pais().RetornarPaises(_paises)
                //,"nomePais"
                      , "idPais"
                      , "nomePais"
                      );
        }


        #endregion
    }


}