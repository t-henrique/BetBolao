using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using BetBolao.Dao;

namespace BetBolao.Models
{
    public class AutenticacaoSeguranca
    {
        
        public bool PermissaoAcessoUsuarioApostador(int idUsuario)
        {//método sempre retornará true para Usuário apostador
            var dao = new DaoAutenticacaoSeguranca();
            return (dao.PermissaoAcessoUsuarioApostador(idUsuario));
        }


        public bool PermissaoAcessoConfidencialBolao(int idusario, int idbolao)
        {
            var dao = new DaoAutenticacaoSeguranca();
            return (dao.PermissaoAcessoConfidencialBolao(idusario, idbolao));
        }

        public bool PermissaoAcessoConfidencialConvidarAmigos(int idAnfitriao, int idbolao)
        {
            var dao = new DaoAutenticacaoSeguranca();
            return (dao.PermissaoAcessoConfidencialConvidarAmigos(idAnfitriao, idbolao));   
        }
    }
}
