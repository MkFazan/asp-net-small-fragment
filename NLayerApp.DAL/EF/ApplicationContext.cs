using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using NLayerApp.DAL.Entities;

namespace NLayerApp.DAL.EF
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Role> ApplicationRoles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Property> Properties { get; set; }
        public ApplicationContext() : base("DefaultConnection") { }
        public ApplicationContext(string connectionString) : base(connectionString) {}

        //TODO::
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();


            //modelBuilder.Entity<User>()
            //       .HasMany(b => b.Roles)
            //       .WithMany(c => c.Users)
            //       .Map(cs =>
            //       {
            //           cs.MapLeftKey("UserId");
            //           cs.MapRightKey("RoleId");
            //           cs.ToTable("AspNetUserRoles");
            //       });

            modelBuilder.Entity<Product>()
                .HasMany<Property>(s => s.Properties)
                .WithMany(c => c.Products)
                .Map(cs =>
                {
                    cs.MapLeftKey("ProductId");
                    cs.MapRightKey("PropertyId");
                    cs.ToTable("ProductProperties");
                });
        }
    }
}