using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetBolao.Compartilhado.Models;

namespace BetBolao.Compartilhado.Dao
{
    public class DaoApostas :DAO
    {
        #region Insert
        public void CadastrarApostas(Aposta aposta )
        {
            var strQuery = "";
            strQuery += string.Format("declare ");
            strQuery += string.Format(" @apostador integer, ");
            strQuery += string.Format(" @jogo integer, ");
            strQuery += string.Format(" @bolao integer, ");
            strQuery += string.Format(" @placarTimeMandante integer, ");
            strQuery += string.Format(" @placarTimeVisitante integer; ");
            strQuery += string.Format(" set @apostador = '{0}' ", aposta.Apostador );
            strQuery += string.Format(" set @jogo = '{0}' ", aposta.Jogo);
            strQuery += string.Format(" set @bolao = '{0}' ", aposta.Bolao);
            strQuery += string.Format(" set @placarTimeMandante = '{0}' ", aposta.PlacarTimeMandante);
            strQuery += string.Format(" set @placarTimeVisitante = '{0}' ", aposta.PlacarTimeVisitante);
            strQuery += string.Format(" if(select count(*) from apostasBolao where apostador = @apostador and jogo = @jogo and bolao = @bolao) = 0 ");
            strQuery += string.Format(" begin ");
            strQuery += string.Format(" insert into apostasBolao values(@apostador, @jogo, @bolao, @placarTimeVisitante, @placarTimeMandante); ");
            strQuery += string.Format(" end ");
            strQuery += string.Format(" else ");
            strQuery += string.Format(" begin ");
            strQuery += string.Format(" update apostasBolao set placarTimeVisitante = @placarTimeVisitante, placarTimeMandante = @placarTimeMandante ");
            strQuery += string.Format(" where apostador = @apostador and jogo = @jogo and bolao = @bolao ; ");
            strQuery += string.Format(" end ");
            
            ExecutaComandoSemRetorno(strQuery);
        }

        #endregion

        #region Update

        #endregion
    }
}
