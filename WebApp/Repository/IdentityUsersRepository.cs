using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebApp.Models;

namespace WebApp.Repository
{
    public class IdentityUsersRepository : IRepository<ApplicationUser>
    {
        ApplicationDbContext db;
        public IdentityUsersRepository()
        {
            db = new ApplicationDbContext();
        }

        public void Create(ApplicationUser item)
        {
            db.Users.Add(item);
        }

        public void Delete(string id)
        {
            var user = db.Users.Find(id);
            if (user != null)
                db.Users.Remove(user);
            db.SaveChanges();
        }

        public ApplicationUser GetItem(string id)
        {
            return db.Users.Find(id);
        }

        public List<ApplicationUser> GetItemsList()
        {
            return db.Users.ToList();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(ApplicationUser item)
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

        public ApplicationUser GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}