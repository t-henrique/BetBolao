using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetBolao.Compartilhado.Dao;

namespace BetBolao.Models
{
    class DaoPaises : DAO
    {
        public string CadastrarPais(Pais pais)
        {
            string retorno = "";
            if (verificarPaisJaCadastrado(pais.NomePais.ToUpper()))
            {
                return retorno += string.Format("O país '{0}' já está cadastrado no Banco de dados!", pais.NomePais);
            }
            else
            {
                var strQuery = "";
                strQuery += "Insert into pais (nomePais, abreviacao)";
                strQuery += string.Format("Values('{0}','{1}')", pais.NomePais.ToUpper(), pais.Abreviacao.ToUpper());

                ExecutaComandoSemRetorno(strQuery);

                return retorno += string.Format("O País '{0}' foi cadastrado com sucesso!", pais.NomePais);
            }
        }

        public void AtualizarPais(Pais pais)
        {
            var strQuery = "";
            strQuery += "Update pais set";
            strQuery += string.Format(" nomePais = '{0}',", pais.NomePais.ToUpper());
            strQuery += string.Format(" abreviacao = '{0}'", pais.Abreviacao).ToUpper();
            strQuery += string.Format(" where id = {0};", pais.IdPais);

            ExecutaComandoSemRetorno(strQuery);

        }

        public bool verificarPaisJaCadastrado(string nomePais)
        {
            base.AbreConexao();

            bool resultado;
            var strQuery = "";
            strQuery += string.Format("Select nomePais from Pais ");
            strQuery += string.Format(" where upper(nomePais) = '{0}'", nomePais);

            var cmd = new SqlCommand(strQuery, Conexao);
            var paisRetornado = Convert.ToString(cmd.ExecuteScalar());
            

            base.Dispose();

            if (paisRetornado == nomePais)
            {
                resultado = true;
                return resultado;
            }
            else
                resultado = false;

            return resultado;
        }

        public bool verificarPaisJaCadastrado(string nomePais, int idPais)
        {
            bool resultado;
            base.AbreConexao();
            string nomeRetornado="";
            var strQuery = "";
            strQuery += string.Format("Select nomePais from Pais ");
            strQuery += string.Format(" where nomePais = '{0}' ", nomePais);
            strQuery += string.Format(" and id <> '{0}' ", idPais);

            var cmd = new SqlCommand(strQuery, Conexao);

            nomeRetornado += string.Format("{0}", base.ExecutaComandoScalar(strQuery));
            
            base.Dispose();

            if (nomeRetornado == nomePais.ToUpper())
            {
                resultado = true;
            }
            else
                resultado = false;

            return resultado;
        }

        public List<Pais> SelecionarPaisesCadastrados()
        {
            base.AbreConexao();
            var paises = new List<Pais>();
            var strQuery = "";
            strQuery += string.Format("Select * from Pais order by nomePais");
            var cmd = new SqlCommand(strQuery, Conexao);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                var pais = new Pais();
                pais.NomePais = dr["nomePais"].ToString();
                pais.IdPais = Convert.ToInt32(dr["id"]);
                pais.Abreviacao = dr["abreviacao"].ToString();
                paises.Add(pais);
            }

            base.Dispose();

            return paises;
        }

        public Pais SelecionarPaisPorId(int id)
        {
            base.AbreConexao();

            var pais = new Pais();
            var strQuery = "";
            strQuery += string.Format("Select * from Pais where id = '{0}' ", id);
            var cmd = new SqlCommand(strQuery, Conexao);
            SqlDataReader dr = cmd.ExecuteReader();



            while (dr.Read())
            {
                pais.NomePais = dr["nomePais"].ToString();
                pais.IdPais = Convert.ToInt32(dr["id"]);
                pais.Abreviacao = dr["abreviacao"].ToString();
            }

            base.Dispose();

            return pais;
        }


       

        public List<Time> RetornarTimesPorPais(int id)
        {
            base.AbreConexao();

            var times = new List<Time>();
            var strQuery = "";
            strQuery += string.Format("Select t.id, t.nomeTime, t.escudo, p.abreviacao, t.idPais ");
            strQuery += string.Format(" from Times t, Pais p ");
            strQuery += string.Format(" where t.idPais = '{0}' ", id);
            strQuery += string.Format(" and t.idPais = p.id");
            var cmd = new SqlCommand(strQuery, Conexao);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                var time = new Time();
                time.NomeTime = dr["nomeTime"].ToString();
                time.IdTime = Convert.ToInt32(dr["id"]);
                time.IdPais = Convert.ToInt32(dr["idPais"]);
                times.Add(time);
            }

            base.Dispose();

            return times;
        }

        public string RetornarAbreviacaoPaisPorId(int id)
        {
            string resultado = "";
            base.AbreConexao();
            var strQuery = "";
            strQuery += string.Format("Select abreviacao ");
            strQuery += string.Format(" from Pais ");
            strQuery += string.Format(" where id = '{0}' ", id);

            resultado += string.Format("{0}", base.ExecutaComandoScalar(strQuery));

            //var cmd = new SqlCommand(strQuery, conexao);

            //resultado = Convert.ToString(cmd.ExecuteScalar());

            base.Dispose();

            return resultado;
        }



        public string RetornarNomePaisPorId(int id)
        {
            string resultado = "";
            base.AbreConexao();
            var strQuery = "";
            strQuery += string.Format("Select nomePais ");
            strQuery += string.Format(" from Pais ");
            strQuery += string.Format(" where id = '{0}' ", id);

            resultado += string.Format("{0}", base.ExecutaComandoScalar(strQuery));

            //var cmd = new SqlCommand(strQuery, conexao);

            //resultado = Convert.ToString(cmd.ExecuteScalar());

            base.Dispose();

            return resultado;
        }
    }
}
