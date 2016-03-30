using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BetBolao.Compartilhado.Dao
{
    //protected class DAO : IDisposable
    public class DAO 
    {
        protected SqlConnection Conexao;

        protected void AbreConexao()
        {
            Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["BDConnection"].ConnectionString);
            Conexao.Open();
        }
        protected void ExecutaComandoSemRetorno(string strQuery)
        {
            AbreConexao();

            var cmdComando = new SqlCommand
            {
                CommandText = strQuery,
                CommandType = CommandType.Text,
                Connection = Conexao
            };
            try
            {
                cmdComando.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                cmdComando.Dispose();
                Dispose();
            }
        }


        protected string ExecutaComandoScalar(string strQuery)
        {
            AbreConexao();
            string retornoScalar ="";
            var cmdComando = new SqlCommand
            {
                CommandText = strQuery,
                CommandType = CommandType.Text,
                Connection = Conexao
            };
            try
            {
                retornoScalar +=string.Format("{0}",cmdComando.ExecuteScalar());
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                cmdComando.Dispose();
                Dispose();    
            }
            return retornoScalar;
        }

        protected void Dispose()
        {
            if (Conexao.State == ConnectionState.Open)
                Conexao.Close();
        }


    }
}