﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IR_Task.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class IRdbEntities : DbContext
    {
        public IRdbEntities()
            : base("name=IRdbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AllPage> AllPages { get; set; }
        public virtual DbSet<bigram_index> bigram_index { get; set; }
        public virtual DbSet<inverted_index> inverted_index { get; set; }
        public virtual DbSet<soundex_index> soundex_index { get; set; }
    }
}
