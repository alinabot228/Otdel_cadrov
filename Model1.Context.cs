﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp1
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class алиначкаEntities : DbContext
    {
        public алиначкаEntities()
            : base("name=алиначкаEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Вакансии> Вакансии { get; set; }
        public virtual DbSet<Должности> Должности { get; set; }
        public virtual DbSet<Компании> Компании { get; set; }
        public virtual DbSet<Полы> Полы { get; set; }
        public virtual DbSet<Соискатели> Соискатели { get; set; }
        public virtual DbSet<Сотрудники> Сотрудники { get; set; }
    
        public virtual int remove_duplicate_spaces(string table_name)
        {
            var table_nameParameter = table_name != null ?
                new ObjectParameter("table_name", table_name) :
                new ObjectParameter("table_name", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("remove_duplicate_spaces", table_nameParameter);
        }
    }
}