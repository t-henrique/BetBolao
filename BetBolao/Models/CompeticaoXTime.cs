using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetBolao.Compartilhado.Dao;

namespace BetBolao.Models
{
    public class CompeticaoXTime
    {
        #region private var
        private DataTable _dt = new DataTable();

        private readonly DaoCompeticao _dao = new DaoCompeticao();


        #endregion

        #region properties
        [Display(Name = "ID de Registro:")]

        public int IdRegistro { get; set; }

        [Display(Name = "ID da Competicao:")]
        public int IdCompeticao { get; set; }

        [Display(Name = "Time:")]
        public int IdTime { get; set; }

        #endregion

        #region public methods
       
        public string AssociarTimeCompeticao(CompeticaoXTime comp)
        {
            string resultado = "";
            try
            {
                _dao.AssociarTimeCompeticao(comp);
                resultado = String.Format("O clube foi associado na competição!");
                return resultado;
            }
            catch (Exception ex)
            {
                resultado = String.Format(ex.ToString());
                return resultado;
            }
        }
        public string DetalharCompeticao(int id, DataTable dt)
        {
            string resultado = "";
            try
            {
                _dao.ListarCompeticoesXTimesPorId(id, dt);
            }
            catch (Exception ex)
            {
                resultado += String.Format("'{0}'", ex.ToString());
            }
            return resultado;
        }

        public string DeletarEquipeDeCompeticao(int idRegistro)
        {
            string resultado = "";

            try
            {
                _dao.RemoveTimeCompeticao(idRegistro);
                resultado += string.Format("O time foi removido desta competição com sucesso!");
                return resultado;

            }
            catch(Exception ex)
            {
                resultado +=string.Format("'{0}'", ex.ToString());
                return resultado;

            }
        }

        #endregion
    }
}
