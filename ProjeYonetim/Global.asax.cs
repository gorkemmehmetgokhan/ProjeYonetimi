using ProjeYonetim.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace ProjeYonetim
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            SetRouteMaps();
        }

        private void SetRouteMaps()
        {
            RouteTable.Routes.MapPageRoute("Varsayilan", "", "~/frmAnasayfa.aspx");
            RouteTable.Routes.MapPageRoute("Anasayfa", "anasayfa", "~/frmAnasayfa.aspx");
            RouteTable.Routes.MapPageRoute("Giris", "giris", "~/frmGiris.aspx");
            RouteTable.Routes.MapPageRoute("UyeOl", "uyeol", "~/frmUyeOl.aspx");
            RouteTable.Routes.MapPageRoute("Cikis", "cikis", "~/frmCikis.aspx");
            RouteTable.Routes.MapPageRoute("Proje", "proje/{id-proje}-{projead}", "~/frmProje.aspx");
            RouteTable.Routes.Ignore("{resource}.axd/{*pathInfo}");
        }
    }
}