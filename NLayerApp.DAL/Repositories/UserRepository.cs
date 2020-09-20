using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
    public class UserRepository : IRepository<User>
    {
        private ApplicationContext db;

        public UserRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users.ToList();
        }

        public User Get(string id)
        {
            return db.Users.Find(id);
        }

        public void Create(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }

        public void Update(string id, User userUpdate)
        {
            var user = db.Users.SingleOrDefault(o => o.Id == id);
            if (user != null)
            {
                user.UserName = userUpdate.UserName;
                db.SaveChanges();
            }
        }
        public IEnumerable<User> Find(Func<User, Boolean> predicate)
        {
            return db.Users.Where(predicate).ToList();
        }
        public void Delete(string id)
        {
            User user = db.Users.Find(id);
            if (user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
        }
    }
}
