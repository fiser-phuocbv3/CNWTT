﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CNWTT
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class cnwttEntities : DbContext
    {
        public cnwttEntities()
            : base("name=cnwttEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<accessory> accessories { get; set; }
        public virtual DbSet<account> accounts { get; set; }
        public virtual DbSet<accountInfor> accountInfors { get; set; }
        public virtual DbSet<bill> bills { get; set; }
        public virtual DbSet<billDetail> billDetails { get; set; }
        public virtual DbSet<desktop> desktops { get; set; }
        public virtual DbSet<feedback> feedbacks { get; set; }
        public virtual DbSet<laptop> laptops { get; set; }
        public virtual DbSet<manufacturer> manufacturers { get; set; }
        public virtual DbSet<news> news { get; set; }
        public virtual DbSet<phone> phones { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<tablet> tablets { get; set; }
    }
}
