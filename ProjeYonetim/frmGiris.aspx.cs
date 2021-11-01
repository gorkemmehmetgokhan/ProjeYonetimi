using ProjeYonetim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjeYonetim
{
    public partial class frmGiris : System.Web.UI.Page
    {
        public Araclar myAraclar = new Araclar();

        //Kullanıcı Authentication işlemi
        public tbl_Kullanici Login(tbl_Kullanici obj)
        {
            string sifreHash = new Araclar().CreateSHA512Hash(obj.Sifre);

            tbl_Kullanici myKullanici = myAraclar.DbContext.tbl_Kullanici.Where(k => k.Eposta == obj.Eposta && k.Sifre == sifreHash).FirstOrDefault();

            if (myKullanici != null)
            {
                return myKullanici;
            }
            else
            {
                return null;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["Kullanici"] != null)
            {
                Response.Redirect("/anasayfa");
            }
        }

        protected void btnGiris_Click(object sender, EventArgs e)
        {
            tbl_Kullanici myKullanici = new tbl_Kullanici
            {
                Eposta = Request.Form["Eposta"],
                Sifre = Request.Form["Sifre"]
            };

            System.Web.HttpContext.Current.Session.Remove("Kullanici");

            myKullanici = Login(myKullanici);

            if (myKullanici != null)
            {
                System.Web.HttpContext.Current.Session["Kullanici"] = myKullanici;

                Response.Redirect("/anasayfa");
            }
            else
            {
                lblGirisUyari.Visible = true;
            }
        }
    }
}