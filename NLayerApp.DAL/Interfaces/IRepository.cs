using System;
using System.Collections.Generic;

namespace NLayerApp.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(string id);
        IEnumerable<T> Find(Func<T, Boolean> predicate);
        void Create(T item);
        void Update(string id, T item);
        void Delete(string id);
    }
}