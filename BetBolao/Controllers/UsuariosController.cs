using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BetBolao.Compartilhado.Models;
using BetBolao.Models;

namespace BetBolao.Controllers
{
      [Authorize]
    public class UsuariosController : Controller
    {
        #region private variables
        private List<Time> _times = new List<Time>();
        private Usuarios _usuario = new Usuarios();
        private DaoUsuarios _dao = new DaoUsuarios();

        #endregion
        // GET: Usuarios

        #region metodos get
        [AllowAnonymous]
        public ActionResult Participar()
        {
            ViewBag.nomeTime = CarregaTodosTimes();
            if (TempData["mensagem"] is Nullable)
            {
                var mensagem = TempData["mensagem"] as string;
            }
            return View();
        }
        [Authorize]
        public ActionResult Editar()
        {
            {
                _usuario.Id = (int)@Session["id"];
                _usuario.Email = (string)@Session["email"];

                _usuario.RetornaUsuario(_usuario);
                ViewBag.nomeTime = CarregaTodosTimes();
            }
            return View(_usuario);
        }



        #endregion

        #region metodos post

        //[HttpPost]
        ////public ActionResult editarMultiplosUsuarios(FormCollection frm)
        //public ActionResult editarMultiplosUsuarios(List<Usuarios> listaUsuarios )
        //{
        //    return RedirectToAction("editarMultiplosUsuarios");
        //}

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken] //método para não deixar outros sites postarem no site
        public ActionResult Participar(Usuarios usuario)
        {

            if (usuario.Email == null) // permanescerá aqui caso o usuário desabilitar o javascritp no client
            {
                ModelState.AddModelError("", "O campo email não pode ser vazio!");
            }
            else
            {
                if (_dao.ValidarEmailDisponivel(usuario.Email))
                {
                    if (ModelState.IsValid)
                    {
                        //var dao = new DaoUsuarios();
                        _dao.SalvarUsuario(usuario);
                        //return RedirectToAction("ManterLogIn", "Conta", usuario);
                        return RedirectToAction("LogOn", "Conta");
                    }
                }
                else
                    ModelState.AddModelError("", string.Format("O email informado - '{0}' - já está cadastrado. Insira outro email!", usuario.Email));
                ViewBag.nomeTime = CarregaTodosTimes();
            }
            return View(usuario);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Editar(Usuarios usuario)
        {
            if (usuario.Email == null) // permanescerá aqui caso o usuário desabilitar o javascritp no client
            {
                ModelState.AddModelError("", "O campo email não pode ser vazio!");
            }
            else
            {

                //if (_dao.ValidarEmailDisponivel(usuario.Email, usuario.Id))
                if (_usuario.VerificarAlteracaoEmailAtualUsuario(usuario.Email, usuario.Id))
                {
                    if (ModelState.IsValid)
                    {
                        //var dao = new DaoUsuarios();
                        _dao.SalvarUsuario(usuario);
                        return RedirectToAction("ManterLogIn", "Conta", usuario);
                    }
                }
                else
                    ModelState.AddModelError("", string.Format("O email informado - '{0}' - já está cadastrado. Por favor insira outro email!", usuario.Email));
                ViewBag.nomeTime = CarregaTodosTimes();
            }
            return View(usuario);
        }
        //escrever assim [HttpPost] ou assim [AcceptVerbs(HttpVerbs.Post)] dá na mesma, pois o primeiro é a simplificação do segundo

        // editar o usuario logado
        #endregion

        #region private methods

        private SelectList CarregaTodosTimes()
        {
            return new SelectList(new Time().RetornaTimes(_times)
                , "nomeTime"
                , "nomeTime"
                );
        }

        #endregion

    }
}