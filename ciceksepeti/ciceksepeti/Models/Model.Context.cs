﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ciceksepetiEntities : DbContext
    {
        public ciceksepetiEntities()
            : base("name=ciceksepetiEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<admin> admin { get; set; }
        public virtual DbSet<kategoriler> kategoriler { get; set; }
        public virtual DbSet<kullanici> kullanici { get; set; }
        public virtual DbSet<resimler> resimler { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<urunler> urunler { get; set; }
        public virtual DbSet<sepet> sepet { get; set; }
        public virtual DbSet<sepeturunliste> sepeturunliste { get; set; }
        public virtual DbSet<Satis> Satis { get; set; }
    }
}
