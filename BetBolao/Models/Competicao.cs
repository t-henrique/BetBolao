using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using BetBolao.Compartilhado.Dao;

namespace BetBolao.Models
{
    public class Competicao
    {

        #region private variables
        private readonly DaoCompeticao _dao = new DaoCompeticao();


        #endregion

        #region Properties
        
        [Display(Name = "ID:")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nome:")]
        public string NomeCompeticao { get; set; }

        [Display(Name = "Ano:")]
        //[DataType(DataType.Date)]
        public int Ano { get; set; }

        [Display(Name = "Status Ativo?:")]
        public string Ativo { get; set; }


        #endregion

        #region Public Methods
        public void SalvarCompeticao(Competicao comp)
        {
            _dao.CadastrarCompeticao(comp);
        }

        public List<Competicao> ListarCompeticoes(List<Competicao> lista)
        {
            lista.InsertRange(0, _dao.ListarCompeticoes());
            return lista;
        }

        public Competicao ListarCompeticaoId(Competicao comp)
        {
            return _dao.ListarCompeticoesPorId(comp);
        }

        public void AlterarNomeCompeticao(Competicao comp)
        {
            _dao.AlterarNomeCompeticao(comp);
        }

        public void DesativarCompeticao(Competicao comp)
        {
            _dao.DesativarCompeticao(comp);
        }

        public string DuplicarCompeticao(Competicao comp)
        {
            //if (_dao.validarDuplicacao(comp))
            //{
            //    _dao.duplicarCompeticao(_dao.listarCompeticoesPorID(comp));
            //    return resultado =string.Format("A duplicação foi feita com sucesso!");
            //}
            //return resultado = string.Format("A não pôde ser realizada pois este campeonado já foi duplicado!");
            if (!_dao.ValidarDuplicacaoCompeticao(comp))
                return string.Format("A não pôde ser realizada pois este campeonado já foi duplicado!");
            
            _dao.DuplicarCompeticao(_dao.ListarCompeticoesPorId(comp));
            return string.Format("A duplicação foi feita com sucesso!");
        }

        public SelectList TranformaListaCompeticoesEmSelectList(List<Competicao> competicaoList)
        {
            return new SelectList(competicaoList
                , "id"
                , "nomeCompeticao"
                );
        }
        #endregion
    }
}
