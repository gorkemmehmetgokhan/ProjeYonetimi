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
    
    public partial class tbl_GorevKullanici
    {
        public int id_GorevKullanici { get; set; }
        public int id_Gorev { get; set; }
        public int id_Kullanici { get; set; }
    
        public virtual tbl_Gorev tbl_Gorev { get; set; }
        public virtual tbl_Kullanici tbl_Kullanici { get; set; }
    }
}