﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Okul_YonetimEntities : DbContext
    {
        public Okul_YonetimEntities()
            : base("name=Okul_YonetimEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Dersler> Dersler { get; set; }
        public virtual DbSet<NotOrtalamalari> NotOrtalamalari { get; set; }
        public virtual DbSet<Ogrenci> Ogrenci { get; set; }
        public virtual DbSet<Ogretmen> Ogretmen { get; set; }
        public virtual DbSet<ProjeBilgisi> ProjeBilgisi { get; set; }
        public virtual DbSet<SinavBilgileri> SinavBilgileri { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}
