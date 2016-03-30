using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BetBolao
{
    public class RouteConfig
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
        }
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Apostar",
                url: "Apostas/Apostar/{idCompeticao}/{idbolao}/{resultadoMandante}/{resultadoVisitante}",
                defaults:
                    new
                    {
                        controller = "Apostas",
                        action = "Apostar",
                        idCompeticao = "idCompeticao",
                        idbolao = "idbolao",
                        resultadoMandante = UrlParameter.Optional,
                        resultadoVisitante = UrlParameter.Optional,
                    }
                );

            routes.MapRoute(
                name: "Convites",
                url: "Bolao/ConvidarParticipantes/{idbolao}/{idusuario}/",
                defaults:
                    new
                    {
                        controller = "Bolao",
                        action = "ConvidarParticipantes",
                        idbolao = "idbolao",
                        idanfitriao = "idusuario"
                    }
                );

            //routes.MapRoute(
            //    name: "SairBolao",
            //    url: "{controller}/{action}/{idbolao}/{idusuario}/",
            //    defaults:
            //        new
            //        {
            //            controller = "Bolao",
            //            action = "SairBolao",
            //            idbolao = "idbolao",
            //            idusuario = "idusuario"
            //        }
            //    );

            

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Home", id = UrlParameter.Optional }
            );   
        }
    }
}
