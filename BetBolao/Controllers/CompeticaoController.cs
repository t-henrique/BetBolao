using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using BetBolao.Compartilhado.Dao;
using BetBolao.Models;

namespace BetBolao.Controllers
{
    [Authorize]
    public class CompeticaoController : Controller
    {

        #region private variables

        private readonly Competicao _comp = new Competicao();
        private readonly List<Time> _times = new List<Time>();
        private readonly List<Competicao> _competicoes = new List<Competicao>();
        private readonly CompeticaoXTime _compTime = new CompeticaoXTime();
        private readonly DaoCompeticao _dao = new DaoCompeticao();
        private readonly List<JogosCompeticao> _listaJogos = new List<JogosCompeticao>();

        #endregion


        #region Métodos Get

        [HttpGet]
        public ActionResult ExcluirClubeCompeticao(int idRegistro, int idCompeticao)
        {
            var idusuario = (int)Session["id"];
            var auth = new AutenticacaoSeguranca();
            if (!auth.PermissaoAcessoUsuarioApostador(idusuario))
            {
                ViewBag.id = idCompeticao;
                TempData["mensagem"] = _compTime.DeletarEquipeDeCompeticao(idRegistro);
                var dt = new DataTable();
                _compTime.DetalharCompeticao(idRegistro, dt);
                return RedirectToAction("ListarCompeticoes", dt);
            }
            return View("_NaoAutorizado");
        }

        public ActionResult Index()
        {
            var idusuario = (int)Session["id"];
            var auth = new AutenticacaoSeguranca();
            if (!auth.PermissaoAcessoUsuarioApostador(idusuario))
            {
                return RedirectToAction("ListarCompeticoes");
            }
            return View("_NaoAutorizado");
        }
        public ActionResult CadastrarCompeticao()
        {
            var idusuario = (int)Session["id"];
            var auth = new AutenticacaoSeguranca();
            if (!auth.PermissaoAcessoUsuarioApostador(idusuario))
            {
                // ViewBag.anos = carregaAnos(); trabalhar depois nesta correção
                //ViewBag.status = carregaStatus();
                return View();
            }
            return View("_NaoAutorizado");
        }
        public ActionResult ListarCompeticoes()
        {
            var idusuario = (int)Session["id"];
            var auth = new AutenticacaoSeguranca();
            var competicoes = new List<Competicao>();
            _comp.ListarCompeticoes(competicoes);
            if (!auth.PermissaoAcessoUsuarioApostador(idusuario))
            {
                return View(competicoes);
            }
            return View("_VisualiarCompeticoesDisponíveis", competicoes);
        }
        public ActionResult EditarCompeticao(int id)
        {
            var idusuario = (int)Session["id"];
            var auth = new AutenticacaoSeguranca();
            if (!auth.PermissaoAcessoUsuarioApostador(idusuario))
            {
                _comp.Id = id;
                _comp.ListarCompeticaoId(_comp);
                return View(_comp);
            }
            return View("_NaoAutorizado");
        }
        public ActionResult DesativarCompeticao(Competicao comp)
        {
            var idusuario = (int)Session["id"];
            var auth = new AutenticacaoSeguranca();
            if (!auth.PermissaoAcessoUsuarioApostador(idusuario))
            {
                try
                {
                    if (comp.Ano < DateTime.Now.Year)
                    {
                        _comp.DesativarCompeticao(comp);
                        return RedirectToAction("ListarCompeticoes");
                    }
                    else
                    {
                        TempData["mensagem"] = "A competição ainda está no seu ano ativo!";
                        return RedirectToAction("ListarCompeticoes");
                    }
                }
                catch
                {
                    return View("ListarCompeticoes");
                }
            }
            return View("_NaoAutorizado");
        }
        public ActionResult DuplicarCompeticao(Competicao comp)
        {
            var idusuario = (int)Session["id"];
            var auth = new AutenticacaoSeguranca();
            if (!auth.PermissaoAcessoUsuarioApostador(idusuario))
            {
                try
                {

                    TempData["mensagem"] = _comp.DuplicarCompeticao(comp);
                    return RedirectToAction("ListarCompeticoes");
                }
                catch (Exception ex)
                {
                    TempData["mensagem"] = ex.ToString();
                    return RedirectToAction("ListarCompeticoes");
                }
            }
            return View("_NaoAutorizado");

        }


        [HttpGet]
        public ActionResult AssociarTimeCompeticao(int id)
        {
            var idusuario = (int)Session["id"];
            var auth = new AutenticacaoSeguranca();
            if (!auth.PermissaoAcessoUsuarioApostador(idusuario))
            {
                _comp.Id = id;
                ViewBag.Times = CarregaTodosTimes();
                ViewBag.Competicoes = _comp.ListarCompeticaoId(_comp);
                return View(_compTime);
            }
            return View("_NaoAutorizado");
        }
        public ActionResult CadastrarJogosCompeticao(int id)
        {
            var idusuario = (int)Session["id"];
            var auth = new AutenticacaoSeguranca();
            if (!auth.PermissaoAcessoUsuarioApostador(idusuario))
            {
                ViewBag.idCompeticao = id;
                ViewBag.Times = CarregaTodosTimesCompeticao(id);
                var jogos = new JogosCompeticao { IdCompeticao = id };
                jogos.ListarUltimosCincoJogosCompeticao(id);
                return View(jogos);
            }
            return View("_NaoAutorizado");
        }
        public ActionResult DetalharCompeticao(int id, string status)
        {
            var idusuario = (int)Session["id"];
            var auth = new AutenticacaoSeguranca();
            if (!auth.PermissaoAcessoUsuarioApostador(idusuario))
            {
                ViewBag.id = id;
                ViewBag.status = status;
                var dt = new DataTable();
                TempData["mensagem"] = _compTime.DetalharCompeticao(id, dt);
                return View(dt);
            }
            return View("_NaoAutorizado");
        }
        public ActionResult TabelaJogosCompeticao(int id)
        {
            var idusuario = (int)Session["id"];
            var auth = new AutenticacaoSeguranca();
            if (!auth.PermissaoAcessoUsuarioApostador(idusuario))
            {
                ViewBag.id = id;
                var jogos = new JogosCompeticao();
                jogos.ListaJogosCompeticao(id);
                return View(jogos);
            }
            return View("_NaoAutorizado");
        }
        public ActionResult EditarJogo(int id, int idCompeticao)
        {
            var idusuario = (int)Session["id"];
            var auth = new AutenticacaoSeguranca();
            if (!auth.PermissaoAcessoUsuarioApostador(idusuario))
            {
                var jogo = new JogosCompeticao { IdJogo = id };
                ViewBag.Times = CarregaTodosTimesCompeticao(idCompeticao);
                jogo.SelecionaJogoCompeticao(jogo);
                return View(jogo);
            }
            return View("_NaoAutorizado");
        }

        public ActionResult LancarPlacarJogos(int id)
        {
            var idusuario = (int)Session["id"];
            var auth = new AutenticacaoSeguranca();
            if (!auth.PermissaoAcessoUsuarioApostador(idusuario))
            {
                ViewBag.id = id;
                var jogos = new JogosCompeticao();
                //var jogosLancados = new JogosCompeticao().ListaJogosCompeticao(id);
                jogos.ListaJogosCompeticao(id);

                return View(jogos);
            }
            return View("_NaoAutorizado");

        }
        #endregion


        #region métodos Post

        [HttpPost]
        public ActionResult CadastrarCompeticao(Competicao comp)
        {
            comp.SalvarCompeticao(comp);
            return RedirectToAction("CadastrarCompeticao");
        }

        [HttpPost]
        public ActionResult EditarCompeticao(Competicao comp)
        {
            _comp.AlterarNomeCompeticao(comp);
            return RedirectToAction("Index");
        }


        //public ActionResult AssociarTimeCompeticao(int idComp, CompeticaoXTime compTime)
        //compTime.IdCompeticao = idComp;
        //_comp.Id = idComp;

        //var id = idComp;
        [HttpPost]
        public ActionResult AssociarTimeCompeticao(CompeticaoXTime compTime)
        {
            //compTime.IdCompeticao = compTime.IdCompeticao;
            _comp.Id = compTime.IdCompeticao;
            //var id = idComp;
            TempData["mensagem"] = compTime.AssociarTimeCompeticao(compTime);
            ViewBag.Times = CarregaTodosTimes();
            ViewBag.Competicoes = _comp.ListarCompeticaoId(_comp);
            return RedirectToAction("AssociarTimeCompeticao", "Competicao", new { id = compTime.IdCompeticao });
        }

        [HttpPost]
        public ActionResult CadastrarJogosCompeticao(int idCompeticao, JogosCompeticao jogo)
        {
            jogo.IdCompeticao = idCompeticao;
            _dao.InserirJogo(jogo);
            return RedirectToAction("CadastrarJogosCompeticao");
        }

        [HttpPost]
        public ActionResult ExcluirJogosCompeticao(string[] chkbox, int idCompeticao)
        {
            if (chkbox == null) return RedirectToAction("TabelaJogosCompeticao", new { id = idCompeticao });
            try
            {
                for (var i = 0; i < chkbox.Count(); i++)
                {
                    if (chkbox[i] != "")
                    {
                        var valor = Convert.ToInt32(chkbox[i].ToString());
                        _dao.RemoverJogoCompeticao(Convert.ToInt32(chkbox[i].ToString()));
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("TabelaJogosCompeticao", new { id = idCompeticao });
        }

        //public ActionResult MyAction(string myArray)
        //{
        //    var myArrayInt = myArray.Split(',').Select(x => Int32.Parse(x)).ToArray();
        //    return Json(null, JsonRequestBehavior.AllowGet);

        //    //My Action Code Here
        //}

        //public JsonResult ExcluirJogosCompeticao(Array checkboxList)
        //{
        //    const string retCode = "Success";

        //    //Faz alguma coisa e retorna um Json

        //    return Json(new { retorno = retCode });

        [HttpPost]
        public ActionResult LancarPlacarJogos(string[] idJogo, int[] resultadoMandante, int[] resultadoVisitante)
        {
            var jogo = new JogosCompeticao();
            for (int i = 0; i < idJogo.Count(); i++)
            {
                jogo.IdJogo = Convert.ToInt32(idJogo[i]);
                jogo.ResultadoMandante = Convert.ToInt32(resultadoMandante[i]);
                jogo.ResultadoVisitante = Convert.ToInt32(resultadoVisitante[i]);
                jogo.JogoFinalizado = true;
                _dao.LancarPlacarJogo(jogo);
            }
            return RedirectToAction("ListarCompeticoes");
        }

        //}

        [HttpPost]
        public ActionResult EditarJogo(JogosCompeticao jogo, int idJogo, int idCompeticao)
        {
            jogo.IdJogo = idJogo;
            _dao.AlteraHorarioDataJogo(jogo);
            return RedirectToAction("TabelaJogosCompeticao", new { id = idCompeticao });
        }

        #endregion métods post


        #region Private Métodos

        private ArrayList CarregaAnos()
        {
            ArrayList anos = new ArrayList();
            for (int i = -1; i < 2; i++)
            {
                anos.Add(DateTime.Now.AddYears(i).Year);
            }
            return anos;
        }

        private SelectList CarregaTodosTimes()
        {
            return new SelectList(new Time().RetornaTimes(_times)
                , "idTime"
                , "nomeTime"
                );
        }

        private SelectList CarregaTodosTimesCompeticao(int idCompeticao)
        {
            return new SelectList(_dao.ListarTimesCompeticao(idCompeticao)
                , "idTime"
                , "nomeTime"
                );
        }

        private SelectList CarregaTodasCompeticoes()
        {
            return new SelectList(new Competicao().ListarCompeticoes(_competicoes)
                , "id"
                , "nomeCompeticao"
                );
        }


        #endregion


    }
}