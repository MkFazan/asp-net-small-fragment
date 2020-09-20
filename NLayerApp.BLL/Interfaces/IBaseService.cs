using NLayerApp.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NLayerApp.BLL.Interfaces
{
    public interface IBaseService<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(string id);
        IEnumerable<T> Find(Func<T, Boolean> predicate);
        Task<OperationResult> Create(T item);
        Task<OperationResult> Update(string id, T item);
        Task<OperationResult> Delete(string id);
        void Dispose();
    }
}