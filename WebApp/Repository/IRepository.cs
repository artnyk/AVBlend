using System;
using System.Collections.Generic;

namespace WebApp.Repository
{
    interface IRepository<T> : IDisposable
        where T : class
    {
        List<T> GetItemsList();
        T GetItem(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        void Save();
    }
}
