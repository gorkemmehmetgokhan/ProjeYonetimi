using ProjeYonetim.DTO;
using ProjeYonetim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjeYonetim
{
    public partial class frmProje : System.Web.UI.Page
    {
        public Araclar myAraclar = new Araclar();

        public tbl_Kullanici myKullanici;
        public tbl_Proje myProje;
        public ProjeDetay myProjeDetay = new ProjeDetay();
        public List<tbl_ProjeKullanici> myProjeKullanicilar;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session["Kullanici"] == null)
            {
                Response.Redirect("/giris");
            }
            else
            {
                myKullanici = (tbl_Kullanici)System.Web.HttpContext.Current.Session["Kullanici"];

                //Proje ID, URL'den alınarak ilgili proje objesi oluşturulur.
                myProje = myAraclar.DbContext.tbl_Proje.Find(Convert.ToInt32(RouteData.Values["id-proje"].ToString().Split('-').First()));

                myProjeDetay.Listeler = myAraclar.DbContext.tbl_Liste.Where(l => l.id_Proje == myProje.id_Proje).ToList();

                myProjeKullanicilar = myAraclar.DbContext.tbl_ProjeKullanici.Where(pk => pk.id_Proje == myProje.id_Proje).ToList();
            }

            if (Context.Request.HttpMethod == "POST")
            {
                //Görev Kaydet butonuna basıldığında
                if (Request.Form["btnGorevKaydet"] != null)
                {
                    //Formdan alınan veriler ile yeni görev oluşturulur.
                    tbl_Gorev myGorev = new tbl_Gorev()
                    {
                        id_Liste = Convert.ToInt16(Request.Form["id_Liste"]),
                        GorevAd = Request.Form["GorevAd"],
                        Aciklama = Request.Form["Aciklama"],
                        BitisTarihi = Convert.ToDateTime(Request.Form["BitisTarihi"]),
                        OlusturulmaTarihi = DateTime.Now
                    };

                    //Görev kapak resmi yüklenmiş ise
                    if (fuGorevResim.PostedFile.ContentLength != 0)
                    {
                        try
                        {
                            var guid = Guid.NewGuid();

                            fuGorevResim.PostedFile.SaveAs(Server.MapPath("~/Images/Gorev/") + guid + ".jpg");

                            myGorev.GorevResim = "/Images/Gorev/" + guid + ".jpg";
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }

                    myAraclar.DbContext.tbl_Gorev.Add(myGorev);
                    myAraclar.DbContext.SaveChanges();

                    //Eğer görev kaydedilmiş ise
                    if (myGorev.id_Gorev > 0)
                    {
                        tbl_Etiket myEtiket = new tbl_Etiket();

                        //Etiket Ad'larını tutan ve virgül ile birbirinden ayrılmış olan string'i Split metodunu kullanarak listeye çeviriyoruz.
                        List<string> myEtiketList = Request.Form["EtiketAd"].Split(',').ToList();

                        //Etiketleri ilgili görev ile ilişkilendiriyoruz.
                        foreach (string etiketAd in myEtiketList)
                        {
                            myEtiket.id_Gorev = myGorev.id_Gorev;
                            myEtiket.EtiketAd = etiketAd;
                            myEtiket.EtiketRenk = String.Format("{0:X6}", new Random().Next(0x1000000));

                            myAraclar.DbContext.tbl_Etiket.Add(myEtiket);
                            myAraclar.DbContext.SaveChanges();
                        }

                        tbl_GorevKullanici myGorevKullanici = new tbl_GorevKullanici();

                        //Kullanıcı ID'lerini tutan ve virgül ile birbirinden ayrılmış olan string'i Split metodunu kullanarak listeye çeviriyoruz.
                        List<string> myKullaniciIDList = Request.Form["id_Kullanici"].Split(',').ToList();

                        //Seçilmiş kullanıcıları ilgili görev ile ilişkilendiriyoruz. (Görev Ekibi oluşturma işlemi)
                        foreach (string kullaniciID in myKullaniciIDList)
                        {
                            myGorevKullanici.id_Gorev = myGorev.id_Gorev;
                            myGorevKullanici.id_Kullanici = Convert.ToInt32(kullaniciID);

                            myAraclar.DbContext.tbl_GorevKullanici.Add(myGorevKullanici);
                            myAraclar.DbContext.SaveChanges();
                        }

                        //Görev ek dosyası yüklenmiş ise (Attachment)
                        if (fuEkDosya.PostedFile.ContentLength != 0)
                        {
                            try
                            {
                                tbl_EkDosya myDosya = new tbl_EkDosya();

                                fuEkDosya.PostedFile.SaveAs(Server.MapPath("~/Files/") + fuEkDosya.PostedFile.FileName);

                                myDosya.id_Gorev = myGorev.id_Gorev;
                                myDosya.EkDosyaAd = fuEkDosya.PostedFile.FileName;
                                myDosya.EkDosyaURL = "/Files/" + fuEkDosya.PostedFile.FileName;

                                myAraclar.DbContext.tbl_EkDosya.Add(myDosya);
                                myAraclar.DbContext.SaveChanges();
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                        }
                    }

                    Response.Redirect(Request.RawUrl);
                }
                //Görev Sil butonuna basıldığında
                else if (Request.Form["btnGorevSil"] != null)
                {
                    tbl_Gorev myGorev = myAraclar.DbContext.tbl_Gorev.Find(Convert.ToInt32(Request.Form["id_Gorev"]));
                    myAraclar.DbContext.tbl_Gorev.Remove(myGorev);
                    myAraclar.DbContext.SaveChanges();

                    Response.Redirect(Request.RawUrl);
                }
                //Seçili görevleri Devam Eden Listesi'ne taşıma butonuna basıldığında 
                else if (Request.Form["btnSagaTasi1"] != null)
                {
                    tbl_Gorev myGorev = new tbl_Gorev();

                    //Görev ID'lerini tutan ve virgül ile birbirinden ayrılmış olan string'i Split metodunu kullanarak listeye çeviriyoruz.
                    List<string> myGorevIDList = Request.Form["id_Gorev"].Split(',').ToList();

                    if (myGorevIDList[0] == "")
                    {                     
                        return;
                    }
                    else
                    {
                        //Seçilmiş görevleri sağındaki listeye atıyoruz.
                        foreach (string gorevID in myGorevIDList)
                        {
                            myGorev = myAraclar.DbContext.tbl_Gorev.Find(Convert.ToInt32(gorevID));

                            myGorev.id_Liste = myProje.tbl_Liste.Where(l => l.ListeAd == "Devam Eden").FirstOrDefault().id_Liste;

                            myAraclar.DbContext.SaveChanges();

                        }
                    }
                                      
                    Response.Redirect(Request.RawUrl);
                }
                //Seçili görevleri Yapılacaklar Listesi'ne taşıma butonuna basıldığında
                else if (Request.Form["btnSolaTasi1"] != null)
                {
                    tbl_Gorev myGorev = new tbl_Gorev();

                    //Görev ID'lerini tutan ve virgül ile birbirinden ayrılmış olan string'i Split metodunu kullanarak listeye çeviriyoruz.
                    List<string> myGorevIDList = Request.Form["id_Gorev"].Split(',').ToList();

                    if (myGorevIDList[0] == "")
                    {
                        return;
                    }
                    else
                    {
                        //Seçilmiş görevleri solundaki listeye atıyoruz.
                        foreach (string gorevID in myGorevIDList)
                        {
                            myGorev = myAraclar.DbContext.tbl_Gorev.Find(Convert.ToInt32(gorevID));

                            myGorev.id_Liste = myProje.tbl_Liste.Where(l => l.ListeAd == "Yapılacaklar").FirstOrDefault().id_Liste;

                            myAraclar.DbContext.SaveChanges();

                        }
                    }
                    
                    Response.Redirect(Request.RawUrl);
                }
                //Seçili görevleri Yapıldı Listesi'ne taşıma butonuna basıldığında
                else if (Request.Form["btnSagaTasi2"] != null)
                {
                    tbl_Gorev myGorev = new tbl_Gorev();

                    //Görev ID'lerini tutan ve virgül ile birbirinden ayrılmış olan string'i Split metodunu kullanarak listeye çeviriyoruz.
                    List<string> myGorevIDList = Request.Form["id_Gorev"].Split(',').ToList();

                    if (myGorevIDList[0] == "")
                    {
                        return;
                    }
                    else
                    {
                        //Seçilmiş görevleri sağındaki listeye atıyoruz.
                        foreach (string gorevID in myGorevIDList)
                        {
                            myGorev = myAraclar.DbContext.tbl_Gorev.Find(Convert.ToInt32(gorevID));

                            myGorev.id_Liste = myProje.tbl_Liste.Where(l => l.ListeAd == "Yapıldı").FirstOrDefault().id_Liste;

                            myAraclar.DbContext.SaveChanges();

                        }
                    }
                  
                    Response.Redirect(Request.RawUrl);
                }
                //Seçili görevleri Devam Eden Listesi'ne taşıma butonuna basıldığında
                else if (Request.Form["btnSolaTasi2"] != null)
                {
                    tbl_Gorev myGorev = new tbl_Gorev();

                    //Görev ID'lerini tutan ve virgül ile birbirinden ayrılmış olan string'i Split metodunu kullanarak listeye çeviriyoruz.
                    List<string> myGorevIDList = Request.Form["id_Gorev"].Split(',').ToList();

                    if (myGorevIDList[0] == "")
                    {
                        return;
                    }
                    else
                    {
                        //Seçilmiş görevleri solundaki listeye atıyoruz.
                        foreach (string gorevID in myGorevIDList)
                        {
                            myGorev = myAraclar.DbContext.tbl_Gorev.Find(Convert.ToInt32(gorevID));

                            myGorev.id_Liste = myProje.tbl_Liste.Where(l => l.ListeAd == "Devam Eden").FirstOrDefault().id_Liste;

                            myAraclar.DbContext.SaveChanges();

                        }
                    }
                   
                    Response.Redirect(Request.RawUrl);
                }
                //Görev Güncelle butonuna basıldığında
                else if (Request.Form["btnGorevGuncelle"] != null)
                {
                    //Güncellenmek istenen görevin ID'si hiddenField'dan alınır ve ilgili görev objesi oluşturulur.
                    tbl_Gorev myGorev = myAraclar.DbContext.tbl_Gorev.Find(Convert.ToInt32(Request.Form["id_Gorev"]));

                    //Güncellenmek istenen değerler formdan alınır.
                    myGorev.GorevAd = Request.Form["GorevAd"];
                    myGorev.Aciklama = Request.Form["Aciklama"];
                    myGorev.BitisTarihi = Convert.ToDateTime(Request.Form["BitisTarihi"]);

                    //Görev kapak resmi değiştirilecek ise
                    if (fileGorevResim.PostedFile.ContentLength != 0)
                    {
                        try
                        {
                            var guid = Guid.NewGuid();

                            fileGorevResim.PostedFile.SaveAs(Server.MapPath("~/Images/Gorev/") + guid + ".jpg");

                            myGorev.GorevResim = "/Images/Gorev/" + guid + ".jpg";
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }

                    //Görevin etiketleri güncelleneceği zaman eskiler silinip sonrasında yeni yazılanlar kaydedilir.
                    myAraclar.DbContext.tbl_Etiket.RemoveRange(myAraclar.DbContext.tbl_Etiket.Where(et => et.id_Gorev == myGorev.id_Gorev));

                    myAraclar.DbContext.SaveChanges();

                    tbl_Etiket myEtiket = new tbl_Etiket();

                    //Etiket adlarını tutan ve virgül ile birbirinden ayrılmış olan string'i Split metodunu kullanarak listeye çeviriyoruz.
                    List<string> myEtiketList = Request.Form["EtiketAd"].Split(',').ToList();

                    //Etiketleri ilgili görev ile ilişkilendiriyoruz.
                    foreach (string etiketAd in myEtiketList)
                    {
                        myEtiket.id_Gorev = myGorev.id_Gorev;
                        myEtiket.EtiketAd = etiketAd;
                        myEtiket.EtiketRenk = String.Format("{0:X6}", new Random().Next(0x1000000));

                        myAraclar.DbContext.tbl_Etiket.Add(myEtiket);
                        myAraclar.DbContext.SaveChanges();
                    }

                    //Görev'e atanmış kullanıcılar güncelleneceği zaman eskiler silinip sonrasında yeni seçilenler kaydedilir.
                    myAraclar.DbContext.tbl_GorevKullanici.RemoveRange(myAraclar.DbContext.tbl_GorevKullanici.Where(gk => gk.id_Gorev == myGorev.id_Gorev));

                    myAraclar.DbContext.SaveChanges();

                    tbl_GorevKullanici myGorevKullanici = new tbl_GorevKullanici();

                    //Kullanıcı ID'lerini tutan ve virgül ile birbirinden ayrılmış olan string'i Split metodunu kullanarak listeye çeviriyoruz.
                    List<string> myKullaniciIDList = Request.Form["id_Kullanici"].Split(',').ToList();

                    //Seçilmiş kullanıcıları ilgili görev ile ilişkilendiriyoruz. (Görev Ekibi oluşturma işlemi)
                    foreach (string kullaniciID in myKullaniciIDList)
                    {
                        myGorevKullanici.id_Gorev = myGorev.id_Gorev;
                        myGorevKullanici.id_Kullanici = Convert.ToInt32(kullaniciID);

                        myAraclar.DbContext.tbl_GorevKullanici.Add(myGorevKullanici);
                        myAraclar.DbContext.SaveChanges();
                    }

                    //Görev'e yeni ek dosya eklenecek ise
                    if (fileEkDosya.PostedFile.ContentLength != 0)
                    {
                        try
                        {
                            tbl_EkDosya myDosya = new tbl_EkDosya();

                            fileEkDosya.PostedFile.SaveAs(Server.MapPath("~/Files/") + fileEkDosya.PostedFile.FileName);

                            myDosya.id_Gorev = myGorev.id_Gorev;
                            myDosya.EkDosyaAd = fileEkDosya.PostedFile.FileName;
                            myDosya.EkDosyaURL = "/Files/" + fileEkDosya.PostedFile.FileName;

                            myAraclar.DbContext.tbl_EkDosya.Add(myDosya);
                            myAraclar.DbContext.SaveChanges();
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }

                    Response.Redirect(Request.RawUrl);
                }
            }
        }
    }
}