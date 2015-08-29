namespace EscortServiceHouse.Data.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<EscortServiceHouseEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(EscortServiceHouseEntities context)
        {    
            if (!context.Roles.Any())
            {
                this.SeedRoles(context);
            }
        }

        private void SeedRoles(EscortServiceHouseEntities context)
        {
            var role = new IdentityRole()
            {
                Name = "Escort"
            };

            context.Roles.Add(role);

            //role = new IdentityRole()
            //{
            //    Name = "Pimp"
            //};

            //context.Roles.Add(role);

            role = new IdentityRole()
            {
                Name = "Customer"
            };

            context.Roles.Add(role);

            role = new IdentityRole()
            {
                Name = "Admin"
            };

            context.Roles.Add(role);

            role = new IdentityRole()
            {
                Name = "Guest"
            };

            context.Roles.Add(role);

            context.SaveChanges();
        }
    }
}
