using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using EmpManagement.Models;

namespace EmpManagement.DAL
{
    public class EmpContext : DbContext
    {

         public EmpContext()
            : base("EmpContext")
        {
          // Database.SetInitializer<EmpContext>(new MigrateDatabaseToLatestVersion<EmpContext, EmpManagement.Migrations.Configuration>());
            Database.SetInitializer<EmpContext>(new DropCreateDatabaseIfModelChanges<EmpContext>());
        }

        public DbSet<Employee> emp { get; set; }
        public DbSet<Dept> Dept { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
              modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
