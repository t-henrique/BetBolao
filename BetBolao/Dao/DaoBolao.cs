using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;
using BetBolao.Compartilhado.Dao;
using BetBolao.Models;
using BetBolao.ViewModel;

namespace BetBolao.Dao
{
    public class DaoBolao : DAO
    {
        #region métodos insert

        public void CadastarBolao(Bolao bolao)
        {
            var strQuery = "";
            strQuery += "declare " +
                        " @nomebolao varchar(50)," +
                        " @idUsuarioCriador int," +
                        " @idCampeonatoAssociado int ";
            strQuery += string.Format("set @nomebolao = '{0}' ", bolao.NomeBolao);
            strQuery += string.Format("set @idUsuarioCriador = '{0}' ", bolao.UsuarioCriadorBolao);
            strQuery += string.Format("set @idCampeonatoAssociado = '{0}' ", bolao.IdCampeonatoAssociado);
            strQuery += "Insert into Bolao(nomebolao, idUsuarioCriador, idCampeonatoAssociado) ";
            strQuery += string.Format("Values(@nomebolao, @idUsuarioCriador, @idCampeonatoAssociado)");

            ExecutaComandoSemRetorno(strQuery);

        }
        #endregion

        #region list

        public List<Bolao> ListarBoloesPorUsuarioCriador(int idUsuario)
        {
            base.AbreConexao();
            var boloes = new List<Bolao>();
            var strQuery = "";
            strQuery += string.Format("Select * from bolao ");
            strQuery += string.Format("where idUsuarioCriador = '{0}'", idUsuario);
            var cmd = new SqlCommand(strQuery, Conexao);
            var dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                var bolao = new Bolao
                {
                    Id = Convert.ToInt32(dr["idbolao"].ToString()),
                    NomeBolao = dr["nomeBolao"].ToString(),
                    UsuarioCriadorBolao = Convert.ToInt32(dr["idUsuarioCriador"]),
                    IdCampeonatoAssociado = Convert.ToInt32(dr["idCampeonatoAssociado"])
                };
                boloes.Add(bolao);
            }

            base.Dispose();

            return boloes;
        }

        public List<Bolao> ListarBoloesPorUsuarioParticipante(int idUsuario)
        {
            base.AbreConexao();
            var boloes = new List<Bolao>();
            var strQuery = "";
            strQuery += string.Format("Select * from bolao b inner join participantesbolao pb on b.idbolao = pb.idbolao where pb.idparticipante = '{0}'", idUsuario);
            var cmd = new SqlCommand(strQuery, Conexao);
            var dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                var bolao = new Bolao (dr["participando"].ToString())
                {
                    Id = Convert.ToInt32(dr["idbolao"].ToString()),
                    NomeBolao = dr["nomeBolao"].ToString(),
                    UsuarioCriadorBolao = Convert.ToInt32(dr["idUsuarioCriador"]),
                    IdCampeonatoAssociado = Convert.ToInt32(dr["idCampeonatoAssociado"])
                    
                };
                boloes.Add(bolao);
            }

            base.Dispose();

            return boloes;
        }

        public List<Bolao> ListarCompeticoes()
        {
            //var competicoes = new List<Competicao>

            base.AbreConexao();
            var boloes = new List<Bolao>();
            var strQuery = "";
            strQuery += string.Format("Select * from bolao ");
            var cmd = new SqlCommand(strQuery, Conexao);
            var dr = cmd.ExecuteReader();


            while (dr.Read())
            {
                var bolao = new Bolao
                {
                    Id = Convert.ToInt32(dr["idbolao"].ToString()),
                    NomeBolao = dr["nomeBolao"].ToString(),
                    UsuarioCriadorBolao = Convert.ToInt32(dr["idUsuarioCriador"]),
                    IdCampeonatoAssociado = Convert.ToInt32(dr["idCampeonatoAssociado"])
                };
                boloes.Add(bolao);
            }

            base.Dispose();

            return boloes;
        }

        //public List<ParticipantesBolao> ListarParticipantesPorBolao(int id)
        //{
        //    AbreConexao();
        //    var participantesBolao = new List<ParticipantesBolao>();
        //    var strQuery = "";
        //    strQuery += string.Format("select * from participantesbolao where idbolao = '{0}' order by idparticipante;", id);
        //    var cmd = new SqlCommand(strQuery, Conexao);
        //    var dr = cmd.ExecuteReader();

        //    while (dr.Read())
        //    {
        //        var participanteBolao = new ParticipantesBolao
        //        {
        //            Id = Convert.ToInt32(dr["id"].ToString()) ,
        //            IdBolao = Convert.ToInt32(dr["idbolao"].ToString()),
        //            IdParticipante = Convert.ToInt32(dr["idparticipante"]),
        //            Pontuacao = Convert.ToInt32(dr["pontuacao"])
        //        };
        //        participantesBolao.Add(participanteBolao);
        //    }

        //    Dispose();

        //    return participantesBolao;
        //}

        public List<DTOParticipantesBolao> ListarParticipantesPorBolao(int id)
        {
            AbreConexao();
            var participantesBolao = new List<DTOParticipantesBolao>();
            var strQuery = "";
            strQuery += string.Format("select p.id, p.idbolao, b.nomeBolao, b.idUsuarioCriador, u.nome as NomeCriador, p.idparticipante, us.nome as NomeParticipante, (select sum(pontuacaoBolao) pontuacao from pegar_pontuacao() join usuarios on usuarios.id = pegar_pontuacao.apostador where usuarios.id= p.idparticipante and bolao = p.idbolao) as pontuacao from participantesbolao p inner join bolao b on p.idbolao = b.idbolao inner join Usuarios u on b.idUsuarioCriador = u.id inner join Usuarios us on p.idparticipante = us.id where b.idbolao = '{0}' order by p.pontuacao, us.nome;", id);
            var cmd = new SqlCommand(strQuery, Conexao);
            var dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                var participante = new DTOParticipantesBolao
                {
                    Id = Convert.ToInt32(dr["id"].ToString()),
                    IdBolao = Convert.ToInt32(dr["idbolao"].ToString()),
                    NomeBolao = dr["nomebolao"].ToString(),
                    IdUsuarioCriador = Convert.ToInt32(dr["idUsuarioCriador"]),
                    NomeCriador = dr["NomeCriador"].ToString(),
                    IdParticipante = Convert.ToInt32(dr["idparticipante"]),
                    NomeParticipante = dr["nomeparticipante"].ToString(),
                    Pontuacao = dr["pontuacao"].ToString() =="" ? 0 : Convert.ToInt32(dr["pontuacao"])

                };
                participantesBolao.Add(participante);

            }

            Dispose();

            return participantesBolao;
        }
        #endregion

        #region update

        public void AtualizarParticipacaoBolaoParaNao(int idbolao, int idusuario)
        {

            var strQuery = "";
            strQuery += "declare " +
                        " @idbolao int," +
                        " @idusuario int ";
            strQuery += string.Format("set @idbolao = '{0}' ", idbolao);
            strQuery += string.Format("set @idusuario = '{0}' ", idusuario);
            strQuery += "Update participantesbolao  set participando = 'não' ";
            strQuery += string.Format(" where idbolao = @idbolao and idparticipante = @idusuario");

            ExecutaComandoSemRetorno(strQuery);
        }
        #endregion

        #region
        #endregion

        #region
        #endregion

    }
}
