using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using BetBolao.Models;

namespace BetBolao.Controllers
{
    [Authorize]
    public class PaisesController : Controller
    {
        #region variáveis
        // GET: Paises
        private static string _resultado;
        private static bool _habilitar;
        private readonly Pais _pais = new Pais();
        private readonly List<Time> _times = new List<Time>();
        #endregion

        #region get
        public ActionResult Index()
        {
            var idusuario = (int)Session["id"];
            var auth = new AutenticacaoSeguranca();
            if (!auth.PermissaoAcessoUsuarioApostador(idusuario))
            {
                return RedirectToAction("ListarPaisesCadastrados");
            }
            return View("_NaoAutorizado");

        }

        public ActionResult CadastrarPais()
        {
            var idusuario = (int)Session["id"];
            var auth = new AutenticacaoSeguranca();
            if (!auth.PermissaoAcessoUsuarioApostador(idusuario))
            {
                if (_habilitar)
                {
                    _habilitar = false;
                    return View();
                }
                else
                {
                    _resultado = "";
                    return View();
                }
            }
            return View("_NaoAutorizado");
        }


        [DisplayName("Listar Países")]
        public ActionResult ListarPaisesCadastrados()
        {
            var idusuario = (int)Session["id"];
            var auth = new AutenticacaoSeguranca();
            if (!auth.PermissaoAcessoUsuarioApostador(idusuario))
            {
                var dao = new DaoPaises();
                var paises = new List<Pais>();
                //paises.AddRange(dao.paisesCadastrados());
                paises.InsertRange(0, dao.SelecionarPaisesCadastrados());
                return View(paises);
            }
            return View("_NaoAutorizado");
        }

        public ActionResult EditarPais(int id)
        {
            var idusuario = (int)Session["id"];
            var auth = new AutenticacaoSeguranca();
            if (!auth.PermissaoAcessoUsuarioApostador(idusuario))
            {
                if (_habilitar)
                {
                    _habilitar = false;
                }
                else
                {
                    _resultado = "";
                }
                var dao = new DaoPaises();
                var pais = new Pais();
                pais = dao.SelecionarPaisPorId(id);
                return View(pais);
            }
            return View("_NaoAutorizado");
        }


        public ActionResult DetalharPais(int id)
        {
            var idusuario = (int)Session["id"];
            var auth = new AutenticacaoSeguranca();
            if (!auth.PermissaoAcessoUsuarioApostador(idusuario))
            {
                ViewBag.abreviacao = _pais.RetornaAbreviacaoPais(id);
                ViewBag.nomePais = _pais.RetornaNomePais(id);
                return View(_pais.RetornarTimesPorPais(_times, id));
            }
            return View("_NaoAutorizado");
        }

        #endregion
        #region métodos post
        [HttpPost]
        public ActionResult editarPais(Pais pais)
        {
            var idusuario = (int)Session["id"];
            var auth = new AutenticacaoSeguranca();
            if (!auth.PermissaoAcessoUsuarioApostador(idusuario))
            {
                var dao = new DaoPaises();
                if (ModelState.IsValid)
                {
                    var _pais = new Pais();
                    _pais = dao.SelecionarPaisPorId(pais.IdPais);

                    if ((_pais.NomePais == pais.NomePais.ToUpper()) & (_pais.Abreviacao == pais.Abreviacao.ToUpper()))
                    {
                        ModelState.AddModelError("",
                            "Os dados não foram alterados, pois você não alterou nenhum dado deste país!");
                    }
                    else
                    {
                        if (dao.verificarPaisJaCadastrado(pais.NomePais, pais.IdPais))
                        {
                            ModelState.AddModelError("",
                                "Os dados não puderam ser alterado pois já existe um país com o mesmo nome!");
                        }
                        else
                        {
                            dao.AtualizarPais(pais);
                            return RedirectToAction("ListarPaisesCadastrados");
                        }
                    }
                }
                return View(pais);
            }
            return View("_NaoAutorizado");
        }

        [HttpPost]
        public ActionResult CadastrarPais(Pais pais)
        {
            var idusuario = (int)Session["id"];
            var auth = new AutenticacaoSeguranca();
            if (!auth.PermissaoAcessoUsuarioApostador(idusuario))
            {

                var dao = new DaoPaises();
                if (ModelState.IsValid)
                {
                    string resultado = dao.CadastrarPais(pais);
                    PaisesController._resultado = resultado;
                    TempData["mensagem"] = PaisesController._resultado;
                    _habilitar = true;
                    return RedirectToAction("CadastrarPais");

                }
                return View();
            }
            return View("_NaoAutorizado");
        }

        #endregion

    }
}