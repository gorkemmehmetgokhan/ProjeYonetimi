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
    
    public partial class tbl_Gorev
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Gorev()
        {
            this.tbl_EkDosya = new HashSet<tbl_EkDosya>();
            this.tbl_Etiket = new HashSet<tbl_Etiket>();
            this.tbl_GorevKullanici = new HashSet<tbl_GorevKullanici>();
        }
    
        public int id_Gorev { get; set; }
        public short id_Liste { get; set; }
        public string GorevAd { get; set; }
        public string Aciklama { get; set; }
        public System.DateTime OlusturulmaTarihi { get; set; }
        public System.DateTime BitisTarihi { get; set; }
        public string GorevResim { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_EkDosya> tbl_EkDosya { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Etiket> tbl_Etiket { get; set; }
        public virtual tbl_Liste tbl_Liste { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_GorevKullanici> tbl_GorevKullanici { get; set; }
    }
}
