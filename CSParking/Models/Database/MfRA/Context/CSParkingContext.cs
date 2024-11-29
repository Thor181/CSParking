using CSParking.Configuration.Database.Interfaces;
using CSParking.Models.Database.Base;
using CSParking.Models.Database.MfRA;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSParking.Models.Database.CsParking.Context
{
    public class CSParkingContext : DbContext
    {

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<CardEvent> CardEvents { get; set; }
        public virtual DbSet<QrEvent> QrEvents { get; set; }
        public virtual DbSet<PayType> PayTypes { get; set; }
        public virtual DbSet<EventType> EventsTypes { get; set; }
        public virtual DbSet<Places> Places { get; set; }

        public CSParkingContext()
        {

        }

        public CSParkingContext(DbContextOptions<CSParkingContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = Assembly.GetEntryAssembly();

            modelBuilder.ApplyConfigurationsFromAssembly(assembly, x => x.GetType().IsAssignableTo(typeof(ICsParkingEntityConfiguration)));

            base.OnModelCreating(modelBuilder);
        }

    }
}
