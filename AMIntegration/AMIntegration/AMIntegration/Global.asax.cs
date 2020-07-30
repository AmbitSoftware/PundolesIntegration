using System;
using System.Net;
using System.Web;
using System.Web.Http;
using AMIntegration.Configuration;

namespace AMIntegration
{
    public class Global : HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; // .NET 4.5
            GlobalConfiguration.Configure(AmIntegrationConfig.Register);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Headers["X-Content-Type-Options"] = "nosniff";
            HttpContext.Current.Response.Headers["X-Frame-Options"] = "DENY";
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}