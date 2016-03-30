using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetBolao.Compartilhado.Dao;

namespace BetBolao.Dao
{
    public class DaoAutenticacaoSeguranca : DAO
    {
        public bool PermissaoAcessoUsuarioApostador(int idUsuario)
        {//método sempre retornará true para Usuário apostador ou usuário
            AbreConexao();
            var strQuery = "";
            strQuery += string.Format("Select perfil from usuarios where id = '{0}';", idUsuario);
            var cmd = new SqlCommand(strQuery, Conexao);
            var retorno = cmd.ExecuteScalar();
            if ((string) retorno == "apostador")
            {
                Dispose();
                return true;
            }
            Dispose();
            return false;
        }

        public bool PermissaoAcessoConfidencialBolao(int idUsuario, int idBolao)
        {
            AbreConexao();
            var strQuery = "";
            strQuery += string.Format("select TOP 1 * from participantesbolao where participando = 'sim' and idbolao = '{0}' and idparticipante = '{1}';",  idBolao, idUsuario);
            var cmd = new SqlCommand(strQuery, Conexao);
            var dr = cmd.ExecuteReader();
            
            var retorno = dr.HasRows;

            Dispose();

            return retorno;


        }
        public bool PermissaoAcessoConfidencialConvidarAmigos(int idAnfitriao, int idBolao)
        {
            AbreConexao();
            var strQuery = "";
            strQuery += string.Format(" select TOP 1 * from participantesbolao where participando = 'sim' and idbolao = '{0}' and idparticipante = '{1}';", idBolao, idAnfitriao);
            var cmd = new SqlCommand(strQuery, Conexao);
            var dr = cmd.ExecuteReader();
            
            var retorno = dr.HasRows;
            Dispose();

            return retorno;
        }

        
        
        //private bool RetornoQuery
    }
}
