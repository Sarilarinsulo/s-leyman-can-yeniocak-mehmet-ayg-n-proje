//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ciceksepeti.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class resimler
    {
        public int resim_id { get; set; }
        public Nullable<int> urun_id { get; set; }
        public string resim_yol { get; set; }
        public Nullable<bool> resim_bit { get; set; }
    
        public virtual urunler urunler { get; set; }
    }
}
