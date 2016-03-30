using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BetBolao.Compartilhado.Dao;
using BetBolao.Dao;

namespace BetBolao.Models
{
    public class Bolao
    {
        private DaoBolao _dao = new DaoBolao();

        public Bolao()
        {
            
        }
        public Bolao(string participando)
        {
            Participando = participando;
        }

        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "O nome do bolão não pode ter menos que 4 caracteres ou ser maior que 50 caracteres.")]
        [Display(Name = "Nome do Bolao:")]
        public string NomeBolao { get; set; }
        
        public int Id { get; set; }
        //public string NomeCompeticao { get; set; }

        public int UsuarioCriadorBolao { get; set; }

        [Display(Name = "Campeonato Associado:")]
        public int IdCampeonatoAssociado { get; set; }

        private string Participando { get; set; } 


        #region metodos public

        public void CadastrarBolao(Bolao bolao)
        {
            _dao.CadastarBolao(bolao);
        }

        public List<Bolao> SelecinarBoloesPartipantes(int id)
        {
            var ListaCompleta = new List<Bolao>();
            var ListaFiltrada = new List<Bolao>();
            ListaCompleta.AddRange(_dao.ListarBoloesPorUsuarioParticipante(id));
            ListaFiltrada.AddRange(ListaCompleta.Where(x=> x.Participando!="não"));

            //var listaFiltrada = listaCompleta.Where(b => b.IdConvidado == idUsuario);

            return ListaFiltrada;

        }


        public void SairBolao(int idbolao, int idusuario)
        {
            _dao.AtualizarParticipacaoBolaoParaNao(idbolao, idusuario);
        }
        //public void ListarCompeticoesPorUsuarioCriador()
        //{
            
        //}


        #endregion
    }
}
