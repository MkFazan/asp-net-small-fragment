namespace NLayerApp.DAL.Migrations
{
    using NLayerApp.DAL.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NLayerApp.DAL.EF.ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(NLayerApp.DAL.EF.ApplicationContext db)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            //db.Phones.Add(new Phone { Name = "Nokia Lumia 630", Company = "Nokia", Price = 220 });
            //db.Phones.Add(new Phone { Name = "iPhone 6", Company = "Apple", Price = 320 });
            //db.Phones.Add(new Phone { Name = "LG G4", Company = "lG", Price = 260 });
            //db.Phones.Add(new Phone { Name = "Samsung Galaxy S 6", Company = "Samsung", Price = 300 });

            //db.ApplicationRoles.Add(new Role { Name = "Admin" });
            //db.ApplicationRoles.Add(new Role { Name = "Manager" });
            
            db.Products.Add(new Product { Id ="2", Name = "pl" });

            db.SaveChanges();
        }
    }
}
