using System;
using NLayerApp.DAL.EF;
using NLayerApp.DAL.Interfaces;
using NLayerApp.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using NLayerApp.DAL.Identity;
using System.Data.Entity;

namespace NLayerApp.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private ApplicationContext db;
        private DbContextTransaction transaction;
        private RoleRepository roleRepository;
        private UserRepository userRepository;
        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;
        
        private ProductRepository productRepository;

        public EFUnitOfWork(string connectionString)
        {
            db = new ApplicationContext(connectionString);
            transaction = db.Database.BeginTransaction();
            userManager = new ApplicationUserManager(new UserStore<User>(db));
            roleManager = new ApplicationRoleManager(new RoleStore<Role>(db));
        }

        public ApplicationUserManager UserManager
        {
            get { return userManager; }
        }

        public ApplicationRoleManager RoleManager
        {
            get { return roleManager; }
        }

        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public IRepository<Role> ApplicationRoles
        {
            get
            {
                if (roleRepository == null)
                    roleRepository = new RoleRepository(db);
                return roleRepository;
            }
        }

        public IRepository<Product> Products
        {
            get
            {
                if (productRepository == null)
                    productRepository = new ProductRepository(db);
                return productRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Commit() 
        {
            transaction.Commit();
        }

        public void Rollback() 
        {
            transaction.Rollback();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}