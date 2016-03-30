using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using BetBolao.Models;

namespace BetBolao.Compartilhado.Dao
{
    public class DaoCompeticao : DAO
    {
        #region insert create
        public void CadastrarCompeticao(Competicao comp)
        {
            var strQuery = "";
            strQuery += string.Format("Insert Into Competicao ");
            strQuery += string.Format("( descricao, ");
            strQuery += string.Format(" ano, ");
            strQuery += string.Format(" status) ");
            strQuery += string.Format("values ('{0}',", comp.NomeCompeticao);
            strQuery += string.Format("'{0}',", comp.Ano);
            strQuery += string.Format("'{0}')", comp.Ativo);

            base.ExecutaComandoSemRetorno(strQuery);

        }
        public void DuplicarCompeticao(Competicao comp)
        {
            var strQuery = "";
            strQuery += string.Format("Insert Into Competicao ");
            strQuery += string.Format("( descricao, ");
            strQuery += string.Format(" ano, ");
            strQuery += string.Format(" status) ");
            strQuery += string.Format("values ('{0}',", comp.NomeCompeticao);
            strQuery += string.Format("'{0}',", comp.Ano += 1);
            strQuery += string.Format("'{0}')", comp.Ativo);
            base.ExecutaComandoSemRetorno(strQuery);
        }
        public void AssociarTimeCompeticao(CompeticaoXTime compTime)
        {
            var strQuery = "";
            strQuery += string.Format("Insert Into Competicao_Time ");
            strQuery += string.Format("( idCompeticao, idTime) ");
            strQuery += string.Format("values ('{0}',", compTime.IdCompeticao);
            strQuery += string.Format("'{0}')", compTime.IdTime);
            base.ExecutaComandoSemRetorno(strQuery);
        }

        public void InserirJogo(JogosCompeticao jogo)
        {
            //DateTime dia;
            //dia = jogo.DataJogo.Date.AddHours(jogo.HorarioJogo.Hour).AddMinutes(jogo.HorarioJogo.Minute).AddSeconds(jogo.HorarioJogo.Second);
            //var dia = jogo.DataJogo.Date.AddHours(jogo.HorarioJogo.Hour).AddMinutes(jogo.HorarioJogo.Minute).AddSeconds(jogo.HorarioJogo.Second);

            //dia += Convert.ToString(jogo.HorarioJogo.TimeOfDay);
            var strQuery = "";
            strQuery += string.Format("Insert into jogosCompeticao");
            strQuery += string.Format(" (idCompeticao,");
            strQuery += string.Format(" idTimeMandante,");
            strQuery += string.Format(" resultadoTimeMandante,");
            strQuery += string.Format(" idTimeVisitante,");
            strQuery += string.Format(" resultadoTimeVisitante,");
            strQuery += string.Format(" datajogo)");
            strQuery += string.Format(" values ('{0}' ,", jogo.IdCompeticao);
            strQuery += string.Format(" '{0}' ,", jogo.IdTimeMandante);
            strQuery += string.Format(" '{0}' ,", jogo.ResultadoMandante);
            strQuery += string.Format(" '{0}' ,", jogo.IdTimeVisitante);
            strQuery += string.Format(" '{0}' ,", jogo.ResultadoVisitante);
            strQuery += string.Format("CONVERT(datetime,'{0}',103 )  )", jogo.DataJogo.Date.AddHours(jogo.HorarioJogo.Hour).AddMinutes(jogo.HorarioJogo.Minute).AddSeconds(jogo.HorarioJogo.Second));

            ExecutaComandoSemRetorno(strQuery);

        }

        #endregion

        #region list
        public Competicao ListarCompeticoesPorId(Competicao comp)
        {
            base.AbreConexao();
            var competicoes = new List<Competicao>();
            var strQuery = "";
            strQuery += string.Format("Select * from Competicao ");
            strQuery += string.Format("where id = '{0}'", comp.Id);
            var cmd = new SqlCommand(strQuery, Conexao);
            var dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                comp.NomeCompeticao = dr["descricao"].ToString();
                comp.Ativo = dr["status"].ToString();
                comp.Id = Convert.ToInt32(dr["id"]);
                comp.Ano = Convert.ToInt32(dr["ano"]);
            }
            base.Dispose();

            return comp;
        }
        public List<Competicao> ListarCompeticoes()
        {
            //var competicoes = new List<Competicao>

            base.AbreConexao();
            var competicoes = new List<Competicao>();
            var strQuery = "";
            strQuery += string.Format("Select * from Competicao order by ano, descricao, status");
            var cmd = new SqlCommand(strQuery, Conexao);
            var dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                var comp = new Competicao();
                comp.NomeCompeticao = dr["descricao"].ToString();
                comp.Ativo = dr["status"].ToString();
                comp.Id = Convert.ToInt32(dr["id"]);
                comp.Ano = Convert.ToInt32(dr["ano"]);
                competicoes.Add(comp);
            }

            base.Dispose();

            return competicoes;
        }

        public JogosCompeticao SelecionaJogoCompeticao(JogosCompeticao jogo)
        {
            base.AbreConexao();
            var strQuery = "";

            strQuery +=
                string.Format(
                    "Select idJogo, idCompeticao, idTimeMandante, resultadoTimeMandante, idTimeVisitante, resultadoTimeVisitante, datajogo from jogosCompeticao where idJogo = '{0}'", jogo.IdJogo);

            var cmd = new SqlCommand(strQuery, Conexao);
            var dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                jogo.IdJogo = Convert.ToInt32(dr["idjogo"]);
                jogo.IdCompeticao = Convert.ToInt32(dr["idcompeticao"]);
                jogo.IdTimeMandante = Convert.ToInt32(dr["idtimemandante"]);
                jogo.ResultadoMandante = Convert.ToInt32(dr["resultadoTimeMandante"]);
                jogo.IdTimeVisitante = Convert.ToInt32(dr["idTimeVisitante"]);
                jogo.ResultadoVisitante = Convert.ToInt32(dr["ResultadoTimeVisitante"]);
                jogo.DataJogo = Convert.ToDateTime(dr["dataJogo"]);
            }

            return jogo;
        }

        public List<JogosCompeticao> ListarJogosTabelaCompeticoes(int idCompeticao, List<JogosCompeticao> tabelaJogos)
        {
            base.AbreConexao();
            var strQuery = "";
            strQuery += string.Format("Select idJogo, idCompeticao, descricao, idTimeMandante, timemandante, resultadoTimeMandante, idTimeVisitante, timevisitante, resultadoTimeVisitante, datajogo, jogofinalizado from tabelajogos where idCompeticao = '{0}' order by datajogo", idCompeticao);
            var cmd = new SqlCommand(strQuery, Conexao);
            var dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                var jogo = new JogosCompeticao
                {
                    IdJogo = Convert.ToInt32(dr["idjogo"]),
                    IdCompeticao = Convert.ToInt32(dr["idcompeticao"]),
                    Competicao = dr["descricao"].ToString(),
                    IdTimeMandante = Convert.ToInt32(dr["idtimemandante"]),
                    NomeTimeMandante = dr["timemandante"].ToString(),
                    ResultadoMandante = Convert.ToInt32(dr["resultadoTimeMandante"]),
                    IdTimeVisitante = Convert.ToInt32(dr["idTimeVisitante"]),
                    NomeTimeVisitante = dr["timevisitante"].ToString(),
                    ResultadoVisitante = Convert.ToInt32(dr["ResultadoTimeVisitante"]),
                    DataJogo = Convert.ToDateTime(dr["dataJogo"]),
                    JogoFinalizado = Convert.ToBoolean(dr["jogoFinalizado"])
                };

                tabelaJogos.Add(jogo);
            }



            base.Dispose();

            return tabelaJogos;
        }

        public List<JogosCompeticao> ListarJogosTabelaApostas(int _idCompeticao, List<JogosCompeticao> tabelaJogos,
            int _idUsuario, int _idbolao)
        {
            //var hoje = DateTime.Now;
            base.AbreConexao();
            var strQuery = "";

            strQuery += string.Format("DECLARE ");
            strQuery += string.Format(" @apostador integer, ");
            strQuery += string.Format(" @idCompeticao integer, ");
            strQuery += string.Format(" @idbolao integer ");
            strQuery += string.Format(" set @apostador = '{0}' ", _idUsuario);
            strQuery += string.Format(" set @idCompeticao = '{0}' ", _idCompeticao);
            strQuery += string.Format(" set @idbolao = '{0}' ", _idbolao);
            strQuery +=
                string.Format(
                    "Select idJogo, idCompeticao, descricao, idTimeMandante, timemandante, (select a.placarTimeMandante from apostasBolao a where a.jogo = idjogo and a.apostador = @apostador and a.bolao = @idbolao ) as placarMandante, idTimeVisitante, timevisitante, (select a.placarTimeVisitante from apostasBolao a where a.jogo = idjogo and a.apostador = @apostador and a.bolao = @idbolao ) as placarVisitante, datajogo, jogofinalizado from tabelajogos t where idCompeticao = @idCompeticao and jogoFinalizado = 'false' ;");

            var cmd = new SqlCommand(strQuery, Conexao);
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var jogo = new JogosCompeticao
                {
                    IdJogo = Convert.ToInt32(dr["idjogo"]),
                    Competicao = dr["descricao"].ToString(),
                    IdCompeticao = Convert.ToInt32(dr["idcompeticao"]),
                    IdTimeMandante = Convert.ToInt32(dr["idTimeMandante"]),
                    NomeTimeMandante = dr["timemandante"].ToString(),
                    ResultadoMandante = dr["placarMandante"].ToString() == "" ? 0 : Convert.ToInt32(dr["placarMandante"]),
                    IdTimeVisitante = Convert.ToInt32(dr["idTimeVisitante"]),
                    NomeTimeVisitante = dr["timevisitante"].ToString(),
                    ResultadoVisitante = dr["placarVisitante"].ToString() == "" ? 0 : Convert.ToInt32(dr["placarVisitante"]),
                    DataJogo = Convert.ToDateTime(dr["dataJogo"]),
                    JogoFinalizado = Convert.ToBoolean(dr["jogoFinalizado"])
                };
                tabelaJogos.Add(jogo);

            }
            base.Dispose();
            return tabelaJogos;
            //{
            //strQuery += string.Format("Select idJogo, idCompeticao, descricao, idTimeMandante, timemandante, placarTimeMandante, idTimeVisitante, timevisitante, placarTimeVisitante, datajogo, jogofinalizado from tabelajogos tj left join apostasBolao ab on tj.idjogo = ab.jogo where tj.idCompeticao = '{0}' and tj.jogoFinalizado ='false' or (apostador is null or apostador = '{1}') and ab.bolao = '{2}' order by dataJogo;", idCompeticao, idUsuario, idbolao);
            //var cmd = new SqlCommand(strQuery, Conexao);
            //var dr = cmd.ExecuteReader();

            //while (dr.Read())
            //{
            //    var jogo = new JogosCompeticao
            //    {
            //        IdJogo = Convert.ToInt32(dr["idjogo"]),
            //        jogo.Competicao = dr["descricao"].ToString(),
            //        jogo.IdCompeticao = Convert.ToInt32(dr["idcompeticao"]),
            //        jogo.IdTimeMandante = Convert.ToInt32(dr["idTimeMandante"]),
            //        jogo.NomeTimeMandante = dr["timemandante"].ToString(),
            //        jogo.ResultadoMandante = (dr["placarTimeMandante"].ToString() == "") ? 0 : Convert.ToInt32(dr["placarTimeMandante"]),
            //        jogo.IdTimeVisitante = Convert.ToInt32(dr["idTimeVisitante"]),
            //        jogo.NomeTimeVisitante = dr["timevisitante"].ToString(),
            //        jogo.ResultadoVisitante = dr["placarTimeVisitante"].ToString() == "" ? 0: Convert.ToInt32(dr["placarTimeVisitante"]) ,
            //        jogo.DataJogo = Convert.ToDateTime(dr["dataJogo"]),
            //        jogo.JogoFinalizado = Convert.ToBoolean(dr["jogoFinalizado"])
            //    };
            //    tabelaJogos.Add(jogo);
            //}

            //}
            /*
            base.Dispose();
            return tabelaJogos;
            */
        }


        public List<JogosCompeticao> ListarUltimosCincoJogosTabelaCompeticao(int idCompeticao, List<JogosCompeticao> tabelaJogos)
        {
            base.AbreConexao();
            var strQuery = "";
            strQuery += string.Format("select top(5) * from tabelajogos where idCompeticao = '{0}' and idjogo > ((select count(*) from jogosCompeticao)-5) order by  idjogo desc", idCompeticao);
            var cmd = new SqlCommand(strQuery, Conexao);
            var dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                var jogo = new JogosCompeticao
                {
                    IdJogo = Convert.ToInt32(dr["idjogo"]),
                    IdCompeticao = Convert.ToInt32(dr["idcompeticao"]),
                    Competicao = dr["descricao"].ToString(),
                    IdTimeMandante = Convert.ToInt32(dr["idtimemandante"]),
                    NomeTimeMandante = dr["timemandante"].ToString(),
                    ResultadoMandante = Convert.ToInt32(dr["resultadoTimeMandante"]),
                    IdTimeVisitante = Convert.ToInt32(dr["idTimeVisitante"]),
                    NomeTimeVisitante = dr["timevisitante"].ToString(),
                    ResultadoVisitante = Convert.ToInt32(dr["ResultadoTimeVisitante"]),
                    DataJogo = Convert.ToDateTime(dr["dataJogo"]),
                    JogoFinalizado = Convert.ToBoolean(dr["jogoFinalizado"])
                };
                tabelaJogos.Add(jogo);
            }

            base.Dispose();

            return tabelaJogos;
        }
        public List<Time> ListarTimesCompeticao(int idCompeticao)
        {
            base.AbreConexao();
            var times = new List<Time>();
            var strQuery = "";
            strQuery += string.Format("select \"idTime\", \"nomeTime\" from CompeticaoXTimeXCompeticaoTime where \"idCompeticao\" = '{0}' order by \"nomeTime\";", idCompeticao);
            var cmd = new SqlCommand(strQuery, Conexao);
            var dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                var time = new Time();
                time.IdTime = Convert.ToInt32(dr["IdTime"].ToString());
                time.NomeTime = dr["nomeTime"].ToString();
                times.Add(time);
            }

            base.Dispose();

            return times;
        }
        public DataTable ListarCompeticoesXTimesPorId(int id, DataTable dt)
        {
            base.AbreConexao();
            var strQuery = "";
            strQuery += string.Format("select ct.idRegistro as ID, c.descricao as Competição, c.ano as Edição, t.nomeTime as 'Times Participantes' ");
            strQuery += string.Format(" from competicao_time ct, Competicao c, Times t ");
            strQuery += string.Format(" where ct.idCompeticao = '{0}' ", id);
            strQuery += string.Format(" and ct.idCompeticao = c.id");
            strQuery += string.Format(" and ct.idTime = t.id");

            var cmd = new SqlCommand(strQuery, Conexao);

            var dr = cmd.ExecuteReader();

            //DataTable dt = new DataTable();
            dt.Load(dr);
            base.Dispose();
            return dt;
        }
        #endregion

        #region update
        public void AlterarNomeCompeticao(Competicao comp)
        {
            var strQuery = "";
            strQuery += string.Format("UPDATE Competicao ");
            strQuery += string.Format("SET descricao ='{0}'", comp.NomeCompeticao);
            strQuery += string.Format(" where id ='{0}'", comp.Id);

            base.ExecutaComandoSemRetorno(strQuery);
        }
        public void DesativarCompeticao(Competicao comp)
        {
            var strQuery = "";
            strQuery += string.Format("UPDATE Competicao ");
            strQuery += string.Format("SET status ='Não'", comp.NomeCompeticao);
            strQuery += string.Format(" where id ='{0}'", comp.Id);

            base.ExecutaComandoSemRetorno(strQuery);
        }

        public void AlteraHorarioDataJogo(JogosCompeticao jogo)
        {
            var strQuery = "";
            strQuery += string.Format("UPDATE jogosCompeticao SET");
            strQuery += string.Format(" datajogo = CONVERT(datetime,'{0}',103 )",
                jogo.DataJogo.Date.AddHours(jogo.HorarioJogo.Hour)
                    .AddMinutes(jogo.HorarioJogo.Minute)
                    .AddSeconds(jogo.HorarioJogo.Second));
            strQuery += string.Format(" WHERE idjogo ='{0}';", jogo.IdJogo);

            ExecutaComandoSemRetorno(strQuery);
        }

        public void LancarPlacarJogo(JogosCompeticao jogo)
        {
            var strQuery = "";
            strQuery += string.Format("UPDATE jogosCompeticao ");
            strQuery += string.Format("SET resultadoTimeMandante ='{0}' ,", jogo.ResultadoMandante);
            strQuery += string.Format(" resultadoTimeVisitante ='{0}' , ", jogo.ResultadoVisitante);
            strQuery += string.Format(" jogofinalizado ='{0}' ", jogo.JogoFinalizado);
            strQuery += string.Format(" where idjogo ='{0}' ;", jogo.IdJogo);

            base.ExecutaComandoSemRetorno(strQuery);
        }

        #endregion


        #region validate
        public bool ValidarDuplicacaoCompeticao(Competicao comp)
        {
            bool resultado;

            base.AbreConexao();

            var strQuery = "";
            strQuery += string.Format("select * from competicao ");
            strQuery += string.Format("where descricao = '{0}'", comp.NomeCompeticao);
            strQuery += string.Format(" and (ano = '{0}' )", comp.Ano);
            strQuery += string.Format(" and ano + 1 = (select max(ano) from competicao ");
            strQuery += string.Format("where descricao = '{0}')", comp.NomeCompeticao);

            var cmd = new SqlCommand(strQuery, Conexao);
            var dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                resultado = false;
                return resultado;
            }
            resultado = true;
            base.Dispose();

            return resultado;

        }

        #endregion

        #region delete
        public void RemoveTimeCompeticao(int id)
        {
            var strQuery = "";
            strQuery += string.Format("Delete from Competicao_Time ");
            strQuery += string.Format("where idRegistro ='{0}'", id);

            base.ExecutaComandoSemRetorno(strQuery);
        }

        public void RemoverJogoCompeticao(int id)
        {
            var strQuery = "";
            strQuery += string.Format("Delete from jogosCompeticao ");
            strQuery += string.Format("where idjogo ='{0}'", id);

            base.ExecutaComandoSemRetorno(strQuery);

        }

        #endregion
    }
}
