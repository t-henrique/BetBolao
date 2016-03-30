using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetBolao.Compartilhado.Models;

namespace BetBolao.ViewModel
{
    public class DtoConvitesPendentes 
    {
        [Display(Name = "Id Convite")]
        public int IdConvite { get; set; }

        [Display(Name = "Id Bolão")]
        public int IdBolao { get; set; }

        [Display(Name = "Nome Bolão")]
        public string NomeBolao { get; set; }
        
        [Display(Name = "Id Anfitrião")]
        public int IdAnfitriao { get; set; }

        [Display(Name = "Nome Anfitrião")]
        public string NomeAnfitriao { get; set; }

        [Display(Name = "Email Anfitrião")]
        public string EmailAnfitriao { get; set; }

        [Display(Name = "Id Convidado:")]
        public int IdConvidado { get; set; }

        [Display(Name = "Nome do Convidado")]
        public string NomeConvidado { get; set; }

        [Required(ErrorMessage = "Digite o email do convidado!")]
        [StringLength(50, ErrorMessage = "Não pode ultrapassar a quantidade de 50 caracteres.")]
        // habilitar ao término do projeto
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido...")]
        [Display(Name = "Email do Convidado:")]
        public string EmailConvidado { get; set; }

        [Required]
        [Display(Name = "Texto Convite")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "O textod o convite tem que ser no mínimo 4 caracteres ou ser maior que 50 caracteres.")]
        public string TextoConvite { get; set; }

        public string Aberto { get; set; }

        public string Aceite { get; set; }

    }
}

