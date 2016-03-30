using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BetBolao.Compartilhado.Dao;

namespace BetBolao.Models
{
    /// <summary>
    /// esta classe é destinada à representação da view de nome "ViewUnificada_CompeticaoXTimeXCompeticaoTime 
    /// </summary>
    public class JogosCompeticao
    {
        #region variables
        private readonly DaoCompeticao _dao = new DaoCompeticao();
        public List<JogosCompeticao> TabelaJogos = new List<JogosCompeticao>();

        #endregion


        #region public properties 
        
        [Display(Name = "ID Registro:")]
        [ReadOnly(true)]
        public int IdJogo { get; set; }

        [Display(Name = "ID Competição:")]
        [ReadOnly(true)]
        public int IdCompeticao { get; set; }

        [Display(Name = "Competição:")]
        [ReadOnly(true)]
        public string Competicao { get; set; }
        
        [Display(Name = "Mandante:")]
        public int IdTimeMandante { get; set; }
        
        [Display(Name = "Time Mandante:")]
        public string NomeTimeMandante { get; set; }

        [DefaultValue(0)]
        [DisplayName("Resultado Mandante:")]
        [RegularExpression("/([0-99])+/g", ErrorMessage = "Informe um email válido...")]
        [StringLength(2, ErrorMessage = "O login deve ter no máximo 30 caracteres")]
        public int? ResultadoMandante { get; set; }

        [Display(Name = "Visitante:")]
        public int IdTimeVisitante { get; set; }

        [DisplayName("Resultado Visitante:")]
        [DefaultValue(0)]
        [RegularExpression("/([0-99])+/g", ErrorMessage = "Não pode ser registrado letras neste campo")]
        [StringLength(2, ErrorMessage = "Não pode passar de 2 caracteres.")]
        public int? ResultadoVisitante { get; set; }

        [Display(Name = "Time Visitante:")]
        public string NomeTimeVisitante { get; set; }

        [Display(Name = "Data:")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime DataJogo { get; set; }


        [Display(Name = "Horario:")]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}")]
        [DataType(DataType.Time)]
        public DateTime HorarioJogo { get; set; }

        public Boolean JogoFinalizado { get; set; }


        #endregion

        #region public methods

        #region list methods
        
        public List<JogosCompeticao> ListaJogosCompeticao(int id)
        {
            _dao.ListarJogosTabelaCompeticoes(id, TabelaJogos);
            return TabelaJogos;
        }

        public List<JogosCompeticao> ListarUltimosCincoJogosCompeticao(int id)
        {
            //_dao.ListarJogosCompeticoes(id, TabelaJogos);
            _dao.ListarUltimosCincoJogosTabelaCompeticao(id, TabelaJogos);
            return TabelaJogos;
        }
        
        public List<JogosCompeticao> ListaJogosCompeticaoSemanaAtual(int id, int idUsuario, int idbolao)
        {
            var listaCompleta = new List<JogosCompeticao>();
            _dao.ListarJogosTabelaApostas(id, listaCompleta, idUsuario, idbolao);
            TabelaJogos.AddRange(listaCompleta.Where(x => (x.DataJogo < DateTime.Now.AddDays(7)) && (x.DataJogo > DateTime.Now.AddHours(2)) && (!x.JogoFinalizado)));
            
            return TabelaJogos;
        }
        
        #endregion
        
        public JogosCompeticao SelecionaJogoCompeticao(JogosCompeticao jogo)
        {
            return _dao.SelecionaJogoCompeticao(jogo);
        }

        


        #endregion
    }
}
