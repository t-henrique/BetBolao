using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetBolao.Compartilhado.Dao;

namespace BetBolao.Models
{
    public class Time
    {

        private DaoTimes _daoTimes = new DaoTimes();
        //private DAOPaises daoPaises = new DAOPaises();

        //private DAO dao = DAO();

        [Required]
        [Display(Name = "Nome do Time:")]
        public string NomeTime{ get; set; }

        //[Required]
        [Display(Name = "ID do Time:")]
        public int IdTime { get; set; }
        
        [Display(Name = "País de Origem:")]
        //[Required]
        //[DisplayName("País")]
        public int IdPais { get; set; }

        public List<Time> RetornaTimes(List<Time> times)
        {
            times.InsertRange(0, _daoTimes.SelecionarTimesCadastrados());
            return times;
        }

        public Time RetornaTime(int id)
        {
            var time = new Time();
            time = _daoTimes.SelecionarTimePorId(id);
            return time;
        }

        public string AtualizarTime (Time time)
        {
            string resultado;
            resultado = _daoTimes.AtualizarTime(time);

            return resultado;
        }
        
        
    }
}
