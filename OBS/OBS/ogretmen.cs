//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OBS
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ogretmen
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ogretmen()
        {
            this.Dersler = new HashSet<Dersler>();
        }
    
        public int OgretmenID { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Branş { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dersler> Dersler { get; set; }
    }
}
