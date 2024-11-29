using CSParking.Configuration.Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSParking.Models.Database.CsParking.Context
{
    public class MainContext : DbContext
    {
        public virtual DbSet<Places> Places { get; set; }

        public MainContext() : base()
        {
            
        }

        public MainContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = Assembly.GetEntryAssembly();

            modelBuilder.ApplyConfigurationsFromAssembly(assembly, x => x.GetType().IsAssignableTo(typeof(IMainEntityConfiguration)));
        }

    }
}
