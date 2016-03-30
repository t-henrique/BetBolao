using System;
using System.Activities.XamlIntegration;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Services.Protocols;
using BetBolao.Compartilhado.Dao;

namespace BetBolao.Compartilhado.Models
{
    public class Aposta
    {

        private readonly DaoApostas _dao = new DaoApostas();


        #region public properties

        public int IdAposta { get; set; }
        public int Jogo { get; set; }
        public int Bolao { get; set; }

        //[RegularExpression("/([0-99])+/g", ErrorMessage = "Informe um email válido...")]
        public int PlacarTimeMandante { get; set; }
        public int PlacarTimeVisitante { get; set; }
        public int Apostador { get; set; }

        #endregion

        #region Public Methods

        public void LancarPlacarJogo(Aposta aposta)
        {
            _dao.CadastrarApostas(aposta);
        }
        #endregion

    }
}
