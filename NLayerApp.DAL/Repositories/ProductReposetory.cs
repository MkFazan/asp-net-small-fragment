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
    public class ProductRepository : IRepository<Product>
    {
        private ApplicationContext db;

        public ProductRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return db.Products.ToList();
        }

        public Product Get(string id)
        {
            return db.Products.Find(id);
        }

        public void Create(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
        }

        public void Update(string id, Product productUpdate)
        {
            var product = db.Products.SingleOrDefault(cat => cat.Id == id);
            if (product != null)
            {
                product.Name = productUpdate.Name;
                db.SaveChanges();
            }
            
        }
        public IEnumerable<Product> Find(Func<Product, Boolean> predicate)
        {
            return db.Products.Where(predicate).ToList();
        }
        public void Delete(string id)
        {
            Product product = db.Products.Find(id);
            if (product != null)
            {
                db.Products.Remove(product);
                db.SaveChanges();
            }
        }
    }
}
