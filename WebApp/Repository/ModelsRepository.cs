using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Repository
{
    public class ModelsRepository :IRepository<Model>
    {
        private DatabaseContext db;

        public ModelsRepository()
        {
            db = new DatabaseContext();
        }

        public void Create(Model model)
        {
            db.Models.Add(model);
        }

        public void Delete(int id)
        {
            var model = db.Models.Find(id);
            if (model != null)
                db.Models.Remove(model);
        }

        public Model GetItem(int id)
        {
            var models = db.Models.Include(m=>m.User).Include(m => m.Category).ToList();
            var model = (from t in models where t.ModelID == id select t).Single();
            return model;
        }

        public List<Model> GetItemsList()
        {
            return db.Models.Include(m => m.User).Include(m => m.Category).ToList();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Model item)
        {
            db.Entry(item).State = EntityState.Modified;
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

        public static implicit operator ModelsRepository(DatabaseContext v)
        {
            throw new NotImplementedException();
        }
    }
}