using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebApp.Models;

namespace WebApp.Repository
{
    public class UsersRepository : IRepository<User>
    {
        private DatabaseContext db;
        public UsersRepository()
        {
            this.db = new DatabaseContext();
        }
        public void Create(User user)
        {
            db.Users.Add(user);
        }

        public void Delete(int id)
        {
            User user = db.Users.Find(id);
            if (user != null)
                db.Users.Remove(user);
        }

        public User GetItem(int id)
        {
            return db.Users.Find(id);
        }

        public List<User> GetItemsList()
        {
            return db.Users.ToList();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(User user)
        {
            db.Entry(user).State = EntityState.Modified;
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

        public static implicit operator UsersRepository(DatabaseContext v)
        {
            throw new NotImplementedException();
        }
    }
}