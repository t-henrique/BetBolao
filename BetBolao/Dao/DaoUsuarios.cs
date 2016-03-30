using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using BetBolao.Compartilhado.Dao;
using BetBolao.Compartilhado.Models;

namespace BetBolao.Models
{
    public class DaoUsuarios : DAO
    {
        //private DAO dao;

        public void SalvarUsuario(Usuarios usuario)
        {
            if (usuario.Id > 0)
                AlterarUsuario(usuario);
            else
                InserirUsuario(usuario);
        }


        public Boolean Logar(Usuarios usuario)
        {
            bool resultado = false;

            if (ValidarUsuario(usuario) != null)
            {

                resultado = true;
            }

            return resultado;


        }


        //public List<Usuarios> ListarUsuarios(List<Usuarios> listausUsuarios )
        //{
        //    base.AbreConexao();
        //    string strQuery = "";
        //    strQuery += string.Format("Select * from usuarios");
        //    var cmd = new SqlCommand(strQuery, Conexao);


        //    SqlDataReader dr = cmd.ExecuteReader();

        //    while (dr.Read())
        //    {
        //        var usuario = new Usuarios();
        //        usuario.Nome = dr["nome"].ToString();
        //        usuario.Email = dr["email"].ToString();
        //        usuario.Senha= dr["senha"].ToString();
        //        usuario.Id= Convert.ToInt32(dr["id"].ToString());
        //        usuario.DataNascimento= Convert.ToDateTime(dr["datanascimento"].ToString());
        //        listausUsuarios.Add(usuario);
        //    }

        //    base.Dispose();

        //    return listausUsuarios;


        //}
        public bool ValidarEmailDisponivel(string email)
        {
            base.AbreConexao();

            email.Replace(" ", "");
            bool resultado;
            var strQuery = string.Format("Select email from usuarios where upper(email) = upper('{0}')", email);
            var cmdComando = new SqlCommand(strQuery, Conexao);
            SqlDataReader dr = cmdComando.ExecuteReader();

            if (dr.HasRows)
            {
                resultado = false;
            }
            else
                resultado = true;

            base.Dispose();
            return resultado;
        }

        public bool ValidarEmailDisponivel(string email, int id)
        {
            base.AbreConexao();

            email.Replace(" ", "");
            bool resultado;
            var strQuery = string.Format("Select email from usuarios where upper(email) = upper('{0}') and id <> '{1}';", email, id);
            var cmdComando = new SqlCommand(strQuery, Conexao);
            SqlDataReader dr = cmdComando.ExecuteReader();

            if (dr.HasRows)
            {
                resultado = false;
            }
            else
                resultado = true;

            base.Dispose();
            return resultado;


        }

        public Usuarios RetornaUsuario(Usuarios usuario)
        {
            AbreConexao();
            string strQuery = "";
            strQuery += string.Format("declare ");
            strQuery += string.Format("@senhaHash varbinary(100), ");
            strQuery += string.Format("@senha varchar(100) ");

            strQuery += string.Format("set @senha ='{0}' ", usuario.Senha);
            strQuery += string.Format("set @senhaHash = Convert(varbinary(100), hashbytes('SHA1', @senha)) ");

            strQuery += string.Format("select * from usuarios where ");
            strQuery += string.Format("email = '{0}'", usuario.Email);

            SqlCommand command = new SqlCommand(strQuery, Conexao);
            var dr = command.ExecuteReader();

            if (dr.Read())
            {
                usuario.Nome = dr[0].ToString();
                usuario.Email = dr[1].ToString();
                usuario.Perfil = dr["perfil"].ToString();
                //usuario.Senha = dr["senha"].ToString();
                usuario.Id = Convert.ToInt32(dr[3]);
                usuario.DataNascimento = Convert.ToDateTime(dr[4].ToString());
                usuario.Comentarios = dr[5].ToString();
                usuario.TimeFavorito = dr[6].ToString();

            }

            return usuario;


        }

        public int RetornarIdUsuarioPorEmail(string email)
        {
            base.AbreConexao();

            email.Replace(" ", "");
            var strQuery = string.Format("Select id from usuarios where upper(email) = upper('{0}');", email);
            var cmdComando = new SqlCommand(strQuery, Conexao);
            var idUsuarioEmail = cmdComando.ExecuteScalar() != null ? (Int32)cmdComando.ExecuteScalar() : 0;

            base.Dispose();
            return idUsuarioEmail;


        }


        private void AlterarUsuario(Usuarios usuario)
        {
            var strQuery = "";
            strQuery += "declare " +
                        " @nome varchar(50)," +
                        " @email varchar(50)," +
                        " @perfil varchar(50)," +
                        " @senha varchar(100)," +
                        " @senhaHash varbinary(100)," +
                        " @datanascimento datetime," +
                        " @comentarios varchar(1000)," +
                        " @timeFavorito varchar(50)";
            strQuery += string.Format(" set @nome = '{0}'", usuario.Nome);
            strQuery += string.Format(" set @email = '{0}'", usuario.Email);
            strQuery += string.Format(" set @perfil = '{0}'", usuario.Perfil);
            strQuery += string.Format(" set @senha = '{0}'", usuario.Senha);
            strQuery += string.Format(" set @senhaHash = Convert(varbinary(100), hashbytes('SHA1', @senha))");
            strQuery += string.Format(" set @datanascimento = CONVERT(datetime,'{0}',103)", usuario.DataNascimento);
            strQuery += string.Format(" set @comentarios = '{0}'", usuario.Comentarios);
            strQuery += string.Format(" set @timeFavorito = '{0}'", usuario.TimeFavorito);
            strQuery += "update Usuarios set nome = @nome, email = @email, senha = @senhaHash, datanascimento =@datanascimento, comentarios =@comentarios, timefavorito= @timeFavorito";
            strQuery += string.Format(" WHERE id = {0} ;", usuario.Id);


            ExecutaComandoSemRetorno(strQuery);

        }
        private void InserirUsuario(Usuarios usuario)
        {
            var strQuery = "";
            strQuery += "declare " +
                        " @nome varchar(50)," +
                        " @email varchar(50)," +
                        " @perfil varchar(50)," +
                        " @senha varchar(100)," +
                        " @senhaHash varbinary(100)," +
                        " @datanascimento datetime," +
                        " @comentarios varchar(1000)," +
                        " @timeFavorito varchar(50)";
            strQuery += string.Format("set @nome = '{0}'", usuario.Nome);
            strQuery += string.Format("set @perfil = '{0}'", usuario.Perfil);
            strQuery += string.Format("set @email = '{0}'", usuario.Email);
            strQuery += string.Format("set @senha = '{0}'", usuario.Senha);
            strQuery += string.Format("set @senhaHash = Convert(varbinary(100), hashbytes('SHA1', @senha))");
            strQuery += string.Format("set @datanascimento = CONVERT(datetime,'{0}',103)", usuario.DataNascimento);
            strQuery += string.Format("set @comentarios = '{0}'", usuario.Comentarios);
            strQuery += string.Format("set @timeFavorito = '{0}'", usuario.TimeFavorito);
            strQuery += "Insert into Usuarios(nome,email, senha, datanascimento,comentarios,timefavorito,perfil)";
            strQuery += string.Format("Values(@nome, @email, @senhaHash, @datanascimento, @comentarios, @timefavorito, @perfil)");

            ExecutaComandoSemRetorno(strQuery);

        }
        private Usuarios ValidarUsuario(Usuarios usuario)
        {
            AbreConexao();
            string strQuery = "";
            strQuery += string.Format("declare ");
            strQuery += string.Format("@senhaHash varbinary(100), ");
            strQuery += string.Format("@senha varchar(100) ");

            strQuery += string.Format("set @senha ='{0}' ", usuario.Senha);
            strQuery += string.Format("set @senhaHash = Convert(varbinary(100), hashbytes('SHA1', @senha)) ");

            strQuery += string.Format("select * from usuarios where ");
            strQuery += string.Format("email = '{0}' and ", usuario.Email);
            strQuery += string.Format("senha = @senhaHash");


            SqlCommand command = new SqlCommand(strQuery, Conexao);
            var dr = command.ExecuteReader();

            if (dr.Read())
            {
                usuario.Nome = dr[0].ToString();
                usuario.Email = dr[1].ToString();
                usuario.Perfil = dr["perfil"].ToString();
                //usuario.Senha = dr["senha"].ToString();
                usuario.Id = Convert.ToInt32(dr[3]);
                usuario.DataNascimento = Convert.ToDateTime(dr[4].ToString());
                usuario.Comentarios = dr[5].ToString();
                usuario.TimeFavorito = dr[6].ToString();

                return usuario;

            }

            return null;


        }


        public List<Usuarios> ListarUsuariosEmailsIds()
        {
            //var competicoes = new List<Competicao>

            base.AbreConexao();
            var usuarios = new List<Usuarios>();
            var strQuery = "";
            strQuery += string.Format("Select nome, email, id from Usuarios order by id;");
            var cmd = new SqlCommand(strQuery, Conexao);
            var dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                var usu = new Usuarios();
                usu.Id = Convert.ToInt32(dr["id"]);
                usu.Nome = dr["nome"].ToString();
                usu.Email = dr["email"].ToString();
                usuarios.Add(usu);
            }

            base.Dispose();

            return usuarios;
        }
    }
}
