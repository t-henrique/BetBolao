using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BetBolao.Compartilhado.Dao;
using BetBolao.Models;
using BetBolao.ViewModel;

namespace BetBolao.Dao
{
    public class DaoConvite:DAO
    {
        #region insert

        public void CriarConvite(ConvitesBolao convite)
        {
            var strQuery = "";
            strQuery += "declare " +
                        " @idBolao varchar(50)," +
                        " @idAnfitriao int," +
                        " @idConvidado int," +
                        " @textoConvite varchar(50)";
            strQuery += string.Format("set @idBolao = '{0}' ", convite.IdBolao);
            strQuery += string.Format("set @idAnfitriao = '{0}' ", convite.IdAnfitriao);
            strQuery += string.Format("set @idConvidado = '{0}' ", convite.IdConvidado);
            strQuery += string.Format("set @textoConvite = '{0}' ", convite.TextoConvite);
            strQuery += "Insert into ConviteParaParticiparBolao(idBolao, idAnfitriao, idConvidado, textoConvite) ";
            strQuery += string.Format("Values(@idBolao, @idAnfitriao, @idConvidado, @textoConvite)");

            ExecutaComandoSemRetorno(strQuery);
       
        }
        #endregion


        #region listas

        public List<ConvitesBolao> ListarConvitesPorUsuario(int idUsuario)
        {
            base.AbreConexao();
            
            var convites = new List<ConvitesBolao>();
            var strQuery = "";
            strQuery += string.Format("Select * from ConviteParaParticiparBolao ");
            strQuery += string.Format("where idConvidado = '{0}'", idUsuario);
            var cmd = new SqlCommand(strQuery, Conexao);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                var convite = new ConvitesBolao()
                {
                    IdConvite = Convert.ToInt32(dr["idConvite"].ToString()),
                    IdBolao = Convert.ToInt32(dr["idBolao"].ToString()),
                    IdAnfitriao = Convert.ToInt32(dr["idAnfitriao"].ToString()),
                    IdConvidado= Convert.ToInt32(dr["idConvidado"].ToString()),
                    TextoConvite = dr["textoConvite"].ToString(),
                    Aberto = dr["aberto"].ToString()
                };
                convites.Add(convite);
            }

            base.Dispose();

            return convites;
        }

        public List<DtoConvitesPendentes> ListarConvites(int idUsuario)
        {
            base.AbreConexao();

            var convites = new List<DtoConvitesPendentes>();
            var strQuery = "";
            strQuery += string.Format("Select c.idconvite, c.idbolao,  b.nomeBolao, u.id as idAnfitriao, u.nome as nomeAnfitriao, u.email as emailAnfitriao, c.idConvidado, us.nome as nomeConvidado, us.email as emailConvidado, c.textoConvite, c.aberto, c.aceite from ConviteParaParticiparBolao c inner join bolao b on c.idBolao = b.idbolao inner join usuarios u on c.idAnfitriao = u.id inner join Usuarios us on c.idConvidado = us.id where c.idconvidado = '{0}'", idUsuario);
            var cmd = new SqlCommand(strQuery, Conexao);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var convite = new DtoConvitesPendentes
                {
                    IdConvite = Convert.ToInt32(dr["idConvite"].ToString()),
                    IdBolao = Convert.ToInt32(dr["idBolao"].ToString()),
                    NomeBolao = dr["nomeBolao"].ToString(),
                    IdAnfitriao = Convert.ToInt32(dr["idAnfitriao"].ToString()),
                    NomeAnfitriao = dr["nomeAnfitriao"].ToString(),
                    EmailAnfitriao = dr["emailAnfitriao"].ToString(),
                    IdConvidado = Convert.ToInt32(dr["idConvidado"].ToString()),
                    NomeConvidado = dr["nomeConvidado"].ToString(),
                    EmailConvidado = dr["emailConvidado"].ToString(),
                    TextoConvite = dr["textoConvite"].ToString(),
                    Aberto = dr["aberto"].ToString(),
                    Aceite = dr["aceite"].ToString()
                };
                convites.Add(convite);
            }
            base.Dispose();
            return convites;
        }
        
        #endregion

        #region update

        public void FinalizarConvite(string opcao, int idConvite)
        {
            var strQuery = "";
            strQuery += "declare " +
                        " @idconvite int, " +
                        " @aceite varchar(3), " +
                        " @aberto varchar(3) ";
           
                
            if (opcao == "aceitar")
            {
                strQuery += string.Format("set @aceite = 'sim' ");
            }
            else
            {
                strQuery += string.Format("set @aceite = 'não' ");
                }
            strQuery += string.Format("set @idconvite = '{0}' ", idConvite);
            strQuery += string.Format("set @aberto = 'não' ");
            strQuery += " update ConviteParaParticiparBolao set aceite = @aceite, aberto = @aberto where idconvite = @idconvite ";

            ExecutaComandoSemRetorno(strQuery);
        }
        #endregion

        #region
        #endregion
    }
}
