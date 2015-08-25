namespace EscortServiceHouse.Data
{
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