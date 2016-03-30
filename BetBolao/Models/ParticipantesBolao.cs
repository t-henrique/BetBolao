using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using BetBolao.Compartilhado.Models;
using BetBolao.Dao;
using BetBolao.ViewModel;

namespace BetBolao.Models
{
    public class ParticipantesBolao 
    {
        public int Id { get; set; }

        [Display(Name = "Nº bolão")]
        public int IdBolao { get; set; }

        [Display(Name = "Nome do bolão")]
        public string NomeBolao { get; set; }

        [Display(Name = "Nome do participante")]
        public int IdParticipante { get; set; }

        [Display(Name = "Pontução")]
        public int Pontuacao { get; set; }


        public List<DTOParticipantesBolao> ListarBoloesParticipantes(int id)
        {
            var dao = new DaoBolao();
            return dao.ListarParticipantesPorBolao(id);
        }

        
    }
}
