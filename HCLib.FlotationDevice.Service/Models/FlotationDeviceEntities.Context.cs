﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HCLib.FlotationDevice.Service.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FlotationDeviceEntities : DbContext
    {
        public FlotationDeviceEntities()
            : base("name=FlotationDeviceEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Collection> Collections { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<LocationCollection> LocationCollections { get; set; }
        public virtual DbSet<ShelfType> ShelfTypes { get; set; }
        public virtual DbSet<Shelving> Shelvings { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
    }
}
