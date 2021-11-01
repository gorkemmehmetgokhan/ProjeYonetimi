using ProjeYonetim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjeYonetim
{
    public partial class frmCikis : System.Web.UI.Page
    {
        public tbl_Kullanici myKullanici;

        //Session verileri temizlenir.
        public void EndSession()
        {
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            FormsAuthentication.SignOut();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["Kullanici"] != null)
            {
                myKullanici = (tbl_Kullanici)System.Web.HttpContext.Current.Session["Kullanici"];
            }

            EndSession();

            Response.Redirect("/giris");
        }
    }
}