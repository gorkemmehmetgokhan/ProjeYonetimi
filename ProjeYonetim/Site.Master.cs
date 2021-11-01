using ProjeYonetim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjeYonetim
{
    public partial class Site : System.Web.UI.MasterPage
    {
        public tbl_Kullanici myKullanici;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Kullanıcı oturum açmış ise
            if (System.Web.HttpContext.Current.Session["Kullanici"] != null)
            {
                myKullanici = (tbl_Kullanici)System.Web.HttpContext.Current.Session["Kullanici"];
            }
        }
    }
}