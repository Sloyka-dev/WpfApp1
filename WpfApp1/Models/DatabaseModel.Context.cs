﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp1.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RealtorAgentEntities : DbContext
    {
        public RealtorAgentEntities()
            : base("name=RealtorAgentEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Deal> Deal { get; set; }
        public virtual DbSet<Passport> Passport { get; set; }
        public virtual DbSet<Property> Property { get; set; }
        public virtual DbSet<Realtor> Realtor { get; set; }
        public virtual DbSet<Seller> Seller { get; set; }
    }
}
