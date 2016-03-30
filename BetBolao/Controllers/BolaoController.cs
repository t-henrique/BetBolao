using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BetBolao.Compartilhado.Dao;
using BetBolao.Compartilhado.Models;
using BetBolao.Dao;
using BetBolao.Models;
using BetBolao.ViewModel;

namespace BetBolao.Controllers
{
    [Authorize]
    public class BolaoController : Controller
    {
        #region Variables
        private readonly Competicao _competicao = new Competicao();
        private readonly List<Competicao> _competicoes = new List<Competicao>();
        //private readonly List<DTOParticipantesBolao> _convites = new List<DtoConvitesPendentes>();
        private readonly DaoBolao _dao = new DaoBolao();
        private readonly DaoConvite _daoConvite = new DaoConvite();
        private readonly Bolao _bolao = new Bolao();
        private readonly List<Bolao> _boloes = new List<Bolao>();
        private readonly Usuarios _usuarios = new Usuarios();
        private readonly ConvitesBolao _convite = new ConvitesBolao();
        #endregion

        #region métodos GET
        // GET: Bolao
        public ActionResult CadastrarBolao()
        {
            ViewBag.idUsuario = (int)Session["id"];
            ViewBag.competicoes = _competicao.TranformaListaCompeticoesEmSelectList(_competicao.ListarCompeticoes(new List<Competicao>()));
            //ViewBag.competicoes =  TranformaListaCompeticoesEmSelectList(ViewBag.competicoes);
            return View();
        }
        public ActionResult ListarBoloesParticipante()
        {
            _boloes.AddRange(_bolao.SelecinarBoloesPartipantes((int)Session["id"]));
            //_boloes.AddRange(_dao.ListarBoloesParticipante((int)Session["id"]));
            return View(_boloes);

        }
        public ActionResult CadastrarApostas(int id)
        {
            ViewBag.id = id;
            return View(new JogosCompeticao().ListaJogosCompeticao(id));
        }
        public ActionResult ConvidarParticipantes(int idbolao, int idanfitriao)
        {
            var auth = new AutenticacaoSeguranca();
            if (auth.PermissaoAcessoConfidencialConvidarAmigos(idanfitriao, idbolao))
            {
                return View();
            }
            return View("_NaoAutorizado");
        }
        public ActionResult ConvitesPendentes()
        {
            var id = (int)@Session["id"];

            var _convites = new List<DtoConvitesPendentes>();
            _convites.AddRange(_convite.RetornarConvitesPorUsuariosId(id));
            return View(_convites);

        }
        public ActionResult AceitarConvite(int id)
        {
            _convite.AceitarConvite(id);
            return RedirectToAction("ConvitesPendentes");
        }
        public ActionResult RejeitarConvite(int id)
        {
            _convite.RejeitarConvite(id);
            return RedirectToAction("ConvitesPendentes");

        }
        public ActionResult SairBolao(int idbolao)
        {
            var idusuario = Convert.ToInt32(Session["id"].ToString());
            _bolao.SairBolao(idbolao, idusuario);
            return RedirectToAction("ListarBoloesParticipante");

        }

        public ActionResult ListarBolao(int id)
        {
            var autenticacao = new AutenticacaoSeguranca();
            var idUsuario = (int)Session["id"];
            if (autenticacao.PermissaoAcessoConfidencialBolao(idUsuario, id))
            {
                var participantesBolao = new ParticipantesBolao();
                return View(participantesBolao.ListarBoloesParticipantes(id));
            }
            return View("_NaoAutorizado");
        }

        #endregion

        #region métodos Post
        [HttpPost]
        public ActionResult CadastrarBolao(Bolao bolao)
        {
            if (ModelState.IsValid)
            {
                bolao.CadastrarBolao(bolao);
            }
            return RedirectToAction("ListarBoloesParticipante", bolao.UsuarioCriadorBolao);
        }

        [HttpPost]
        public ActionResult ConvidarParticipantes(ConvitesBolao convitesBolao)
        {
            if (ModelState.IsValid)
            {
                var daoUsuarios = new DaoUsuarios();
                if ((convitesBolao.IdConvidado = daoUsuarios.RetornarIdUsuarioPorEmail(convitesBolao.EmailConvidado)) !=
                    0)
                {
                    _daoConvite.CriarConvite(convitesBolao);

                    return RedirectToAction("ConvidarParticipantes", new { idbolao = convitesBolao.IdBolao, idanfitriao = convitesBolao.IdAnfitriao });
                }
                TempData["mensagem"] = ViewBag.mensagem = "O email informado não é correspondente a um usuário do sistema";
            }
            return View();
        }

        #endregion

        #region métodos private

        #endregion
    }
}