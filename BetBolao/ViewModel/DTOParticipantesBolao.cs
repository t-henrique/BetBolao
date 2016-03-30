using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetBolao.ViewModel
{
    public class DTOParticipantesBolao
    {
        

        [Display(Name = "ID Bolão")]
        public int Id { get; set; }

        [Display(Name = "ID Bolão")]
        public int IdBolao { get; set; }

        [Display(Name = "Nome Bolão")]
        public string NomeBolao  { get; set; }

        [Display(Name = "ID Criador Bolão")]
        public int IdUsuarioCriador { get; set; }
        
        [Display(Name = "Nome do Criador")]
        public string NomeCriador { get; set; }

        [Display(Name = "Id Participante")]
        public int IdParticipante { get; set; }

        [Display(Name = "Nome Participante")]
        public string NomeParticipante { get; set; }
        
        [Display(Name = "Pontuação")]
        public int? Pontuacao { get; set; }

       
        
    }
}
