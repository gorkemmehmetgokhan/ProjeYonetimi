//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjeYonetim.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Kullanici
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Kullanici()
        {
            this.tbl_GorevKullanici = new HashSet<tbl_GorevKullanici>();
            this.tbl_Proje = new HashSet<tbl_Proje>();
            this.tbl_ProjeKullanici = new HashSet<tbl_ProjeKullanici>();
        }
    
        public int id_Kullanici { get; set; }
        public short id_KullaniciTur { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string AdSoyad { get; set; }
        public string Eposta { get; set; }
        public string Sifre { get; set; }
        public string Resim { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_GorevKullanici> tbl_GorevKullanici { get; set; }
        public virtual tbl_KullaniciTur tbl_KullaniciTur { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Proje> tbl_Proje { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_ProjeKullanici> tbl_ProjeKullanici { get; set; }
    }
}