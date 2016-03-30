using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;
using BetBolao.Models;

namespace BetBolao.Compartilhado.Models
{
    public class Usuarios
    {

        public readonly DateTime DataAtual = DateTime.Now;
        private readonly DaoUsuarios _dao = new DaoUsuarios();
       
        [Display(Name = "Nome:")]
        [Required(ErrorMessage="Digite seu nome!")]
        [StringLength(50,ErrorMessage="Não pode ultrapassar a quantidade de 50 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage="Digite seu email!")]
        [StringLength(50,ErrorMessage="Não pode ultrapassar a quantidade de 50 caracteres.")]
        // habilitar ao término do projeto
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido...")]
        [Display(Name = "Email:")]
        //[Remote("EmailDisponivel","Usuarios", ErrorMessage = "O email digitado já existe")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite sua senha!")]
        [DataType(DataType.Password)]
        [StringLength(20,MinimumLength = 4, ErrorMessage = "A senha não pode ser menor que 4 caracteres nem maior que 20 caracteres.")]
        [Display(Name = "Senha:")]
        public string Senha { get; set; }

        //[ReadOnly(true)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite sua data de nascimento!")]
        [Display(Name = "Data de Nascimento:")]
        [DataType(DataType.Date)]
        //[MinLength(typeof(DateTime),"1/1/1910")]
        //[DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}")]
        //[ValidacaoCustomizada(DateTime.Today, ErrorMessage = "A data digitada não pode ser igual ou maior que a data atual!")]
        public DateTime? DataNascimento { get; set; }
        

        [DisplayName("Cometarios:")]
        public string Comentarios { get; set; }
        
        [Display(Name="Time do Coração:")]
        public string TimeFavorito { get; set; }

        public string Perfil { get; set; }

        public Boolean LogarUsuario(Usuarios usuario)
        {
            //var usuario = new Usuarios();
            var dao = new DaoUsuarios();

            return dao.Logar(usuario);
            
        }
        public static string GenerateSenhaHasg(string strAssinatura)
        {
            SHA1 hasher = SHA1.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();

            byte[] array = encoding.GetBytes(strAssinatura);
            array = hasher.ComputeHash(array);

            StringBuilder strHexa = new StringBuilder();

            foreach (byte item in array)
            {
                // Convertendo  para Hexadecimal
                strHexa.Append(item.ToString("x2"));
            }
            return strHexa.ToString();
        }

        public Boolean VerificarAlteracaoEmailAtualUsuario(string emailAtual, int id)
        {
            return _dao.ValidarEmailDisponivel(emailAtual, id);
        }
        public Usuarios RetornaUsuario(Usuarios usuario)
        {
           return _dao.RetornaUsuario(usuario);
        }

        public List<Usuarios> ListarCompeticoes(List<Usuarios> lista)
        {
            lista.InsertRange(0, _dao.ListarUsuariosEmailsIds());
            return lista;
        }
        
    }
}
