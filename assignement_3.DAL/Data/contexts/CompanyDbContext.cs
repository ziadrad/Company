using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using assignement_3.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace assignement_3.DAL.Data.contexts
{
    internal class CompanyDbContext:DbContext
    {
        public CompanyDbContext() : base()
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = . ; Database = Compagny ; Trusted_Connection = True ; TrustedServerCertificate = True ; ");
            base.OnConfiguring(optionsBuilder);
        }
       public DbSet<Department> Departments { get; set; }
    }
}
