using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebApp.Models;

namespace WebApp.Repository
{
    public class CategoriesRepository : IRepository<Category>
    {
        private DatabaseContext db;

        public CategoriesRepository()
        {
            db = new DatabaseContext();
        }

        public Category GetItem(int id)
        {
            return db.Categories.Find(id);
        }

        public List<Category> GetItemsList()
        {
            return db.Categories.ToList();
        }

        public void Create(Category category)
        {
            db.Categories.Add(category);
        }

        public void Delete(int id)
        {
            Category category = db.Categories.Find(id);
            if (category != null)
                db.Categories.Remove(category);
        }

        public void Update(Category category)
        {
            db.Entry(category).State = EntityState.Modified;
        }

        public void Save()
        {
            db.SaveChanges();
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
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}