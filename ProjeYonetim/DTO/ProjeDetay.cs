using ProjeYonetim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjeYonetim.DTO
{
    public class ProjeDetay
    {
        public List<tbl_Liste> Listeler { get; set; }
        //public int id_Proje { get; set; }
        //public short? id_Liste { get; set; }
        //public string ListeAd { get; set; }
        //public string Aciklama_Liste { get; set; }
        public List<tbl_Gorev> Gorevler { get; set; }
        //public int? id_Gorev { get; set; }
        //public string GorevAd { get; set; }
        //public string Aciklama_Gorev { get; set; }
        //public System.DateTime? OlusturulmaTarihi { get; set; }
        //public System.DateTime? BitisTarihi { get; set; }
        //public string GorevResim { get; set; }
        public List<tbl_Etiket> Etiketler { get; set; }
        //public int? id_Etiket { get; set; }
        //public string EtiketAd { get; set; }
        //public string EtiketRenk { get; set; }
        public List<tbl_EkDosya> EkDosyalar { get; set; }
        //public int? id_EkDosya { get; set; }
        //public string EkDosyaURL { get; set; }
        public List<tbl_Kullanici> Kullanicilar { get; set; }
        //public int? id_Kullanici { get; set; }
        //public string AdSoyad { get; set; }
        //public string Resim { get; set; }
    }
}