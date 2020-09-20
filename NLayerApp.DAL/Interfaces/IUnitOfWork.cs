using NLayerApp.DAL.Entities;
using NLayerApp.DAL.Identity;
using System;
using System.Threading.Tasks;

namespace NLayerApp.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Role> ApplicationRoles { get; }
        IRepository<User> Users { get; }
        
        IRepository<Product> Products { get; }

        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }

        void Save();
        Task SaveAsync();
        void Commit();
        void Rollback();
    }
}