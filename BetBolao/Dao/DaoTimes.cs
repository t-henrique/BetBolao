using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BetBolao.Models;

namespace BetBolao.Compartilhado.Dao
{
    public class DaoTimes : DAO
    {
        public string CadastrarTime(Time time)
        {
            string retorno = "";
            if (VerificarTimeJaCadastrado(time.NomeTime.ToUpper()))
            {
                var strQuery = "";
                strQuery += "Insert into times (nomeTime, idPais)";
                strQuery += string.Format("Values('{0}','{1}')", time.NomeTime.ToUpper(), time.IdPais);

                base.ExecutaComandoSemRetorno(strQuery);

                return retorno += string.Format("O Time '{0}' foi cadastrado com sucesso!", time.NomeTime.ToUpper());
            }
            else
                return retorno += string.Format("O Time '{0}' já estava cadastrado no banco de dados por isso não pode ser cadastrado!", time.NomeTime.ToUpper());

        }

        private bool VerificarTimeJaCadastrado(string nomeTime)
        {
            base.AbreConexao();

            bool resultado;
            var strQuery = "";
            strQuery += string.Format("Select nomeTime from Times ");
            strQuery += string.Format(" where upper(nomeTime) = '{0}'", nomeTime);

            var cmd = new SqlCommand(strQuery, Conexao);
            var paisRetornado = Convert.ToString(cmd.ExecuteScalar());
            //if (Convert.ToString(cmd) == nomePais.ToString())

            base.Dispose();

            if (paisRetornado == nomeTime)
            {
                resultado = false;
                return resultado;
            }
            resultado = true;
            return resultado;
        }

        public List<Time> SelecionarTimesCadastrados()
        {
            base.AbreConexao();
            var times = new List<Time>();
            var strQuery = "";
            strQuery += string.Format("Select * from Times order by nomeTime");
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


        public Time SelecionarTimePorId(int id)
        {
            base.AbreConexao();
            var time = new Time();
            var strQuery = "";
            strQuery += string.Format("Select * from Times");
            strQuery += string.Format(" where id = '{0}'", id);
            var cmd = new SqlCommand(strQuery, Conexao);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                
                time.NomeTime = dr["nomeTime"].ToString();
                time.IdTime = Convert.ToInt32(dr["id"]);
                time.IdPais = Convert.ToInt32(dr["idPais"]);
            }

            base.Dispose();

            return time;
        }

        public string AtualizarTime(Time time)
        {
            string resultado = "";
            var strQuery = "";
            strQuery += string.Format("Update Times ");
            strQuery += string.Format(" set nomeTime = '{0}'", time.NomeTime.ToUpper());
            strQuery += string.Format(" where id = '{0}'", time.IdTime);
            base.ExecutaComandoSemRetorno(strQuery);

            //string resultado = "";
            //List<Time> _times = new List<Time>();
            //_times.AddRange(_times.retornaTimes(_times));
            //if (times.Contains(new Time{ time.nomeTime, time.idTime, time.idPais}))
            //{
            //    resultado += "O time não pode ser alterado pois já existe um time com este mesmo nome, país.";
            //}
            //else
                resultado += "O time foi atualizado";

            return resultado;
        }

    }
}