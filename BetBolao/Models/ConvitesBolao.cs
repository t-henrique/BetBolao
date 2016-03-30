using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using BetBolao.Compartilhado.Dao;
using BetBolao.Compartilhado.Models;
using BetBolao.Dao;
using BetBolao.ViewModel;

namespace BetBolao.Models
{
    public class ConvitesBolao
    {
        private readonly DaoConvite _daoConvite = new DaoConvite();

        [Display(Name = "Id Convite")]
        public int IdConvite { get; set; }

        [Display(Name = "Id Bolão")]
        public int IdBolao { get; set; }

        [Display(Name = "Id Anfitrião")]
        public int IdAnfitriao { get; set; }

        [Display(Name = "Escreva o email do convidado:")]
        public int IdConvidado { get; set; }

        [Required(ErrorMessage = "Digite o email do convidado!")]
        [StringLength(50, ErrorMessage = "Não pode ultrapassar a quantidade de 50 caracteres.")]
        // habilitar ao término do projeto
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido...")]
        [Display(Name = "Email do Convidado:")]
        public string EmailConvidado { get; set; }

        [Required]
        [Display(Name = "Escreva aqui seu convite")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "O textod o convite tem que ser no mínimo 4 caracteres ou ser maior que 50 caracteres.")]
        public string TextoConvite { get; set; }

        public string Aberto { get; set; }

        public string Aceite { get; set; }



        //public void AceitarConvite()
        //{
        //    _daoConvite.
        //}



        //public IEnumerable<ConvitesBolao> RetornarConvitesPorUsuariosId(int idUsuario)
        //{
        //    List<ConvitesBolao> listaCompleta = new List<ConvitesBolao>();
           
        //        listaCompleta.AddRange(_daoConvite.ListarConvites());

        //        //var listaFiltrada = listaCompleta.Where((ConvitesBolao b) => b.IdConvidado == idUsuario);
        //        var listaFiltrada = listaCompleta.Where(b => b.IdConvidado == idUsuario);

        //    return listaFiltrada;
        //}

        public IEnumerable<DtoConvitesPendentes> RetornarConvitesPorUsuariosId(int idUsuario)
        {
            List<DtoConvitesPendentes> listaCompleta = new List<DtoConvitesPendentes>();

            listaCompleta.AddRange(_daoConvite.ListarConvites(idUsuario));

            //var listaFiltrada = listaCompleta.Where((ConvitesBolao b) => b.IdConvidado == idUsuario);
            var listaFiltrada = listaCompleta.Where(b => b.IdConvidado == idUsuario);

            return listaFiltrada.ToList();
        }


        public SelectList TranformaListaCompeticoesEmSelectList(List<Usuarios> usuarioList)
        {
            return new SelectList(usuarioList
                , "id"
                , "email"
                );
        }

        public void AceitarConvite(int idConvite)
        {
            const string opcao = "aceitar";
            _daoConvite.FinalizarConvite(opcao, idConvite);
        }

        public void RejeitarConvite(int idConvite)
        {
            const string opcao = "rejeitar";
            _daoConvite.FinalizarConvite(opcao, idConvite);
        }
    }
}
