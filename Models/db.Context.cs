﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace cinema.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class cinemaEntities : DbContext
    {
        public cinemaEntities()
            : base("name=cinemaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Bilety> Bilety { get; set; }
        public virtual DbSet<Filmy> Filmy { get; set; }
        public virtual DbSet<Miejsca> Miejsca { get; set; }
        public virtual DbSet<Rezerwacje> Rezerwacje { get; set; }
        public virtual DbSet<Seanse> Seanse { get; set; }
    
        public virtual ObjectResult<SeanseModelView> getSeanse(Nullable<int> idfilmu)
        {
            var idfilmuParameter = idfilmu.HasValue ?
                new ObjectParameter("idfilmu", idfilmu) :
                new ObjectParameter("idfilmu", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SeanseModelView>("getSeanse", idfilmuParameter);
        }
    }
}
