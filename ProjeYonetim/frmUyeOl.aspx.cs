using ProjeYonetim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjeYonetim
{
    public partial class frmUyeOl : System.Web.UI.Page
    {
        public Araclar myAraclar = new Araclar();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUyeOl_Click(object sender, EventArgs e)
        {
            if (Context.Request.HttpMethod == "POST")
            {
                string eposta = Request.Form["Eposta"];

                if (myAraclar.DbContext.tbl_Kullanici.Any(k => k.Eposta == eposta))
                {
                    lblUyeOlUyari.Text = "Girdiğiniz e-posta adresi ile oluşturulmuş üyelik bulunmaktadır. Lütfen farklı bir e-posta deneyiniz.";
                    lblUyeOlUyari.Visible = true;
                    return;
                }

                tbl_Kullanici myKullanici = new tbl_Kullanici();

                myKullanici.id_KullaniciTur = Convert.ToByte(Request.Form["radioKullaniciTur"]);

                myKullanici.Eposta = Request.Form["Eposta"];
                myKullanici.Ad = Request.Form["Ad"];
                myKullanici.Soyad = Request.Form["Soyad"];

                HttpPostedFile myFile = Request.Files["fileProfilResmi"];

                //Dosya seçilmiş ise
                if (myFile != null && myFile.ContentLength > 0)
                {
                    var guid = Guid.NewGuid();

                    //Dosya kaydedilir.
                    string filePath = Server.MapPath("~/Images/Kullanici/") + guid + ".jpg";
                    myFile.SaveAs(filePath);

                    myKullanici.Resim = "/Images/Kullanici/" + guid + ".jpg";
                }

                myKullanici.Sifre = myAraclar.CreateSHA512Hash(Request.Form["Sifre"]);
                myAraclar.DbContext.tbl_Kullanici.Add(myKullanici);
                myAraclar.DbContext.SaveChanges();

                System.Web.HttpContext.Current.Session.Remove("Kullanici");

                if (myKullanici.id_Kullanici > 0)
                {
                    System.Web.HttpContext.Current.Session["Kullanici"] = myAraclar.DbContext.tbl_Kullanici.Find(myKullanici.id_Kullanici);

                    Response.Redirect("/anasayfa");
                }
                else
                {
                    lblUyeOlUyari.Text = "Üyelik oluşturulamadı. Lütfen bilgileri eksiksiz girip tekrar deneyiniz.";
                    lblUyeOlUyari.Visible = true;
                    return;
                }
            }
        }
    }
}