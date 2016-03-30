using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BetBolao.Models
{
    public class Pais
    {

        private DaoPaises _dao = new DaoPaises();


        [Required]
        [DisplayName("Nome:")]
        [StringLength(50)]
        public string NomePais { get; set; }
        
        //[UIHint("readonlyi")]
        [DisplayName("ID cadastro:")]
        //[HiddenInput(DisplayValue = false)]
        //[ReadOnly(true)]
        
        public int IdPais{ get; set; }
        
        [Required]
        [MinLength(3)]
        [MaxLength(3)]
        [StringLength(3,ErrorMessage="A abreviação do País deve conter apenas 3 letras. Ex: BRA, ARG, BOL,EUA...")]
        [DisplayName("Abreviação:")]
        public string Abreviacao { get; set; }


        #region Métodos Construtores

        public Pais()
        {

        }
#endregion 

        public List<Pais> RetornarPaises(List<Pais> list)
        {
            var dao = new DaoPaises();
            list = dao.SelecionarPaisesCadastrados();

            return list;
        }

        public List<Time> RetornarTimesPorPais(List<Time> times, int idPais)
        {
            var dao = new DaoPaises();
            times.InsertRange(0, dao.RetornarTimesPorPais(idPais));

            return times;
        }


        public Pais RetornarPaisDetalhado(int id)
        {
            var pais = new Pais();
            pais = _dao.SelecionarPaisPorId(id);
            return pais;
        }

        public string RetornaAbreviacaoPais(int id)
        {
            string resultado;
            return resultado = _dao.RetornarAbreviacaoPaisPorId(id);
        }

        public string RetornaNomePais(int id)
        {
            string resultado;
            return resultado = _dao.RetornarNomePaisPorId(id);
        }
    }
}