using NLayerApp.DAL.EF;
using NLayerApp.DAL.Entities;
using NLayerApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.DAL.Repositories
{
    public class RoleRepository : IRepository<Role>
    {
        private ApplicationContext db;

        public RoleRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<Role> GetAll()
        {
            return db.ApplicationRoles.ToList();
        }

        public Role Get(string id)
        {
            return db.ApplicationRoles.Find(id);
        }

        public void Create(Role role)
        {
            db.ApplicationRoles.Add(role);
            db.SaveChanges();
        }

        public void Update(string id, Role roleUpdate)
        {
            var role = db.ApplicationRoles.SingleOrDefault(cat => cat.Id == id);
            if (role != null)
            {
                role.Name = roleUpdate.Name;
                db.SaveChanges();
            }
            
        }
        public IEnumerable<Role> Find(Func<Role, Boolean> predicate)
        {
            return db.ApplicationRoles.Where(predicate).ToList();
        }
        public void Delete(string id)
        {
            Role role = db.ApplicationRoles.Find(id);
            if (role != null)
            {
                db.ApplicationRoles.Remove(role);
                db.SaveChanges();
            }
        }
    }
}
