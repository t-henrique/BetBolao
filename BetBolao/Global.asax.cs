using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using BetBolao.Controllers;

namespace BetBolao
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

       protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            //Fires upon attempting to authenticate the use
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.Identity.GetType() == typeof(FormsIdentity))
                    {
                        FormsIdentity fi = (FormsIdentity)HttpContext.Current.User.Identity;
                        FormsAuthenticationTicket fat = fi.Ticket;

                        String[] astrPerfis = fat.UserData.Split('|');
                        HttpContext.Current.User = new GenericPrincipal(fi, astrPerfis);
                    }
                }
            }
            
        }

        //código para manipulção do erro
       protected void Application_Error(object sender, EventArgs e)
       {
           var app = (MvcApplication)sender;
           var context = app.Context;
           var ex = app.Server.GetLastError();
           context.Response.Clear();
           context.ClearError();

           var httpException = ex as HttpException;

           var routeData = new RouteData();
           routeData.Values["controller"] = "errors";
           routeData.Values["exception"] = ex;
           routeData.Values["action"] = "http500";

           if (httpException != null)
           {
               switch (httpException.GetHttpCode())
               {
                   case 404:
                       routeData.Values["action"] = "http404";
                       break;
                   case 500:
                       routeData.Values["action"] = "http500";
                       break;
               }
           }

           IController controller = new ErrorsController();
           controller.Execute(new RequestContext(new HttpContextWrapper(context), routeData));
       }
    }

}
