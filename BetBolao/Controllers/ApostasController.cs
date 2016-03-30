using System;
using System.Linq;
using System.Web.Mvc;
using BetBolao.Compartilhado.Models;
using BetBolao.Models;

namespace BetBolao.Controllers
{
    [Authorize]
    public class ApostasController : Controller
    {
        #region private variables
        private readonly Aposta _aposta = new Aposta();
        #endregion

        #region  get
        // GET: Apostas/Create
        [HttpGet]
        public ActionResult Apostar(int idCompeticao, int idbolao)
        {
            var idUsuario = Convert.ToInt32(Session["id"]);
            var autorizacao = new AutenticacaoSeguranca();
            if (autorizacao.PermissaoAcessoConfidencialBolao(idUsuario, idbolao))
            {
                var jogos = new JogosCompeticao();
                jogos.ListaJogosCompeticaoSemanaAtual(idCompeticao, idUsuario, idbolao);
                return View(jogos);
            }
            return View("_NaoAutorizado");
        }

        #endregion get

        #region post
        [HttpPost]
        [Route(Name = "Apostar")]
        public ActionResult Apostar(string[] idJogo, int[] resultadoMandante, int[] resultadoVisitante, int idbolao)
        {
            try
            {
                var idUsuario = Convert.ToInt32(Session["id"]);
                var aposta = new Aposta();
                for (var i = 0; i < idJogo.Count(); i++)
                {
                    aposta.Jogo = Convert.ToInt32(idJogo[i]);
                    aposta.PlacarTimeMandante = Convert.ToInt32(resultadoMandante[i]);
                    aposta.PlacarTimeVisitante = Convert.ToInt32(resultadoVisitante[i]);
                    aposta.Bolao = Convert.ToInt32(idbolao);
                    aposta.Apostador = idUsuario;
                    _aposta.LancarPlacarJogo(aposta);
                }
                return RedirectToAction("ListarBoloesParticipante","Bolao");
            }
            catch(Exception ex)
            {
                return View();
            }
        }
        
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        #endregion post
    }
}
