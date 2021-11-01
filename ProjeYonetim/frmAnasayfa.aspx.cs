using ProjeYonetim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjeYonetim
{
    public partial class frmAnasayfa : System.Web.UI.Page
    {
        public Araclar myAraclar = new Araclar();

        public tbl_Kullanici myKullanici;
        public List<tbl_Proje> myProjeler;
        public List<tbl_Kullanici> myKullanicilar;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["Kullanici"] == null)
            {
                Response.Redirect("/giris");
            }
            else
            {
                myKullanici = (tbl_Kullanici)System.Web.HttpContext.Current.Session["Kullanici"];

                myProjeler = myAraclar.DbContext.tbl_Proje.Include("tbl_ProjeKullanici").Where(p => p.id_Kullanici == myKullanici.id_Kullanici || p.tbl_ProjeKullanici.Any(pk => pk.id_Kullanici == myKullanici.id_Kullanici)).ToList();

                myKullanicilar = myAraclar.DbContext.tbl_Kullanici.ToList();
            }

            if (Context.Request.HttpMethod == "POST")
            {
                //Proje Kaydet butonuna basıldığında
                if (Request.Form["btnProjeKaydet"] != null)
                {
                    //Formdan alınan veriler ile yeni proje oluşturulur.
                    tbl_Proje myProje = new tbl_Proje();

                    myProje.ProjeAd = Request.Form["ProjeAd"];
                    myProje.Aciklama = Request.Form["Aciklama"];
                    myProje.id_Kullanici = myKullanici.id_Kullanici;
                    myProje.id_ProjeDurumTur = 1;
                    myProje.OlusturmaTarihi = DateTime.Now;

                    //Proje kapak resmi yüklenmiş ise
                    if (fuProjeResim.PostedFile.ContentLength != 0)
                    {                       
                        try
                        {
                            var guid = Guid.NewGuid();

                            fuProjeResim.PostedFile.SaveAs(Server.MapPath("~/Images/Proje/") + guid + ".jpg");

                            myProje.ProjeResim = "/Images/Proje/" + guid + ".jpg";
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }

                    myAraclar.DbContext.tbl_Proje.Add(myProje);
                    myAraclar.DbContext.SaveChanges();

                    //Eğer proje kaydedilmiş ise
                    if (myProje.id_Proje > 0)
                    {
                        //Projenin altındaki görevleri içeren listeler oluşturulur.
                        tbl_Liste myListe = new tbl_Liste();

                        List<string> listeAdlari = new List<string>();
                        listeAdlari.Add("Yapılacaklar");
                        listeAdlari.Add("Devam Eden");
                        listeAdlari.Add("Yapıldı");

                        foreach (string listeAd in listeAdlari)
                        {
                            myListe.id_Proje = myProje.id_Proje;
                            myListe.ListeAd = listeAd;
                            myListe.Aciklama = listeAd + " Listesi";

                            myAraclar.DbContext.tbl_Liste.Add(myListe);
                            myAraclar.DbContext.SaveChanges();
                        }

                        tbl_ProjeKullanici myProjeKullanici = new tbl_ProjeKullanici();

                        //Kullanıcı ID'lerini tutan ve virgül ile birbirinden ayrılmış olan string'i Split metodunu kullanarak listeye çeviriyoruz.
                        List<string> myKullaniciIDList = Request.Form["id_Kullanici"].Split(',').ToList();

                        //Kullanıcının kendisi de proje ile ilişkilendirilir.
                        myKullaniciIDList.Add(myKullanici.id_Kullanici.ToString());

                        //Seçilmiş kullanıcıları ilgili proje ile ilişkilendiriyoruz. (Proje Ekibi oluşturma işlemi)
                        foreach (string kullaniciID in myKullaniciIDList)
                        {
                            myProjeKullanici.id_Proje = myProje.id_Proje;
                            myProjeKullanici.id_Kullanici = Convert.ToInt32(kullaniciID);

                            myAraclar.DbContext.tbl_ProjeKullanici.Add(myProjeKullanici);
                            myAraclar.DbContext.SaveChanges();
                        }

                        Response.Redirect("/anasayfa");
                    }
                }
                //Proje Güncelle butonuna basıldığında
                else if (Request.Form["btnProjeGuncelle"] != null)
                {
                    //Güncellenmek istenen projenin ID'si hiddenField'dan alınır ve ilgili proje objesi oluşturulur.
                    tbl_Proje myProje = myAraclar.DbContext.tbl_Proje.Find(Convert.ToInt32(Request.Form["id_Proje"]));

                    //Güncellenmek istenen değerler formdan alınır.
                    myProje.ProjeAd = Request.Form["ProjeAd"];
                    myProje.Aciklama = Request.Form["Aciklama"];

                    //Proje kapak resmi değiştirilecek ise
                    if (fileProjeResim.PostedFile.ContentLength != 0)
                    {
                        try
                        {
                            var guid = Guid.NewGuid();

                            fileProjeResim.PostedFile.SaveAs(Server.MapPath("~/Images/Proje/") + guid + ".jpg");

                            myProje.ProjeResim = "/Images/Proje/" + guid + ".jpg";
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }

                    //Proje'ye atanmış kullanıcılar güncelleneceği zaman eskiler silinip sonrasında yeni seçilenler kaydedilir.
                    myAraclar.DbContext.tbl_ProjeKullanici.RemoveRange(myAraclar.DbContext.tbl_ProjeKullanici.Where(pk => pk.id_Proje == myProje.id_Proje));

                    myAraclar.DbContext.SaveChanges();

                    tbl_ProjeKullanici myProjeKullanici = new tbl_ProjeKullanici();

                    //Kullanıcı ID'lerini tutan ve virgül ile birbirinden ayrılmış olan string'i Split metodunu kullanarak listeye çeviriyoruz.
                    List<string> myKullaniciIDList = Request.Form["id_Kullanici"].Split(',').ToList();

                    //Kullanıcının kendisi de proje ile ilişkilendirilir.
                    myKullaniciIDList.Add(myKullanici.id_Kullanici.ToString());

                    //Seçilmiş kullanıcıları ilgili proje ile ilişkilendiriyoruz. (Proje Ekibi oluşturma işlemi)
                    foreach (string kullaniciID in myKullaniciIDList)
                    {
                        myProjeKullanici.id_Proje = myProje.id_Proje;
                        myProjeKullanici.id_Kullanici = Convert.ToInt32(kullaniciID);

                        myAraclar.DbContext.tbl_ProjeKullanici.Add(myProjeKullanici);
                        myAraclar.DbContext.SaveChanges();
                    }

                    Response.Redirect("/anasayfa");
                }
            }
        }
    }
}