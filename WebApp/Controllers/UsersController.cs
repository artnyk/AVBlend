using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApp.Filters;
using WebApp.Models;
using WebApp.Repository;

namespace WebApp.Controllers
{
    [Culture]
    public class UsersController : Controller
    {
        IRepository<User> _usersRepository;

        public UsersController()
        {
            _usersRepository = new UsersRepository();       
        }

        public ActionResult Index()
        {  
            return View(_usersRepository.GetItemsList());
        }

        //View models of some useer
        public ActionResult Seller(string id)
        {
            var models = new List<Model>();
            using (var db = new ModelsRepository())
            {
                models = db.GetItemsList();
            }
            var sellerModels = from t in models
                               where t.User.Login == id
                               select t;

            if (sellerModels.Count() > 0)
            {
                return View(sellerModels.ToList());
            }
            return null;
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            return View(_usersRepository.GetItem((int)id));
        }   

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            return View(_usersRepository.GetItem((int)id));
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            return View(_usersRepository.GetItem((int)id));
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = _usersRepository.GetItem(id);
            
            using (ApplicationDbContext adb = new ApplicationDbContext())
            {
                var identityUser = adb.Users.Find(user.IdentityUserId);
                adb.Users.Remove(identityUser);
                adb.SaveChanges();
            }
            _usersRepository.Delete(id);
            _usersRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _usersRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}
