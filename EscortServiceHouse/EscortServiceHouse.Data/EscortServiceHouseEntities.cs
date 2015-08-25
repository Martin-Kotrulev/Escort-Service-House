namespace EscortServiceHouse.Data
{
    using EscortService.Models;
    using EscortService.Models.Users;
    using EscortServiceHouse.Data.Migrations;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;

    public class EscortServiceHouseEntities : IdentityDbContext<ApplicationUser>
    {
        public EscortServiceHouseEntities()
            : base("EscortServiceHouseEntities")
        {
            var strategy =
                new MigrateDatabaseToLatestVersion<EscortServiceHouseEntities, Configuration>();
            Database.SetInitializer(strategy);
        }

        public IDbSet<Appointment> Appointments { get; set; }

        public IDbSet<Picture> Pictures { get; set; }

        public IDbSet<PriceList> PriceLists { get; set; }

        public IDbSet<Review> Reviews { get; set; }

        public IDbSet<Service> Services { get; set; }

        public IDbSet<Escort> Escorts { get; set; }

        public IDbSet<Customer> Customers { get; set; }

        public IDbSet<Admin> Admins { get; set; }

        public static EscortServiceHouseEntities Create()
        {
            return new EscortServiceHouseEntities();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}