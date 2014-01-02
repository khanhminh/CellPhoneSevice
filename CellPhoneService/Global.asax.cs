using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CellPhoneService
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        public static ResourceServerConfiguration config;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            config = new ResourceServerConfiguration
            {
                EncryptionVerificationCertificate = new X509Certificate2(Server.MapPath("~/Certs/localhost.pfx"), "a"),
                IssuerSigningCertificate = new X509Certificate2(Server.MapPath("~/Certs/localhost.cer"))
            };

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}