using System.Linq;
using System.Web.Mvc;
using WebApp.Filters;
using WebApp.Models;
using WebApp.Repository;

namespace WebApp.Controllers
{
    //Controller for managing model categories
    [Culture]
    public class CategoriesController : Controller
    {
        IRepository<Category> _categoriesRepository;
        IRepository<Model> _modelsRepository;

        public CategoriesController()
        {
            _categoriesRepository = new CategoriesRepository();
            _modelsRepository = new ModelsRepository();
        }
        // GET: Categories
        public ActionResult Index()
        {
            return View(_categoriesRepository.GetItemsList()); 
        }

        
        // GET: Category Models
        public ActionResult Models(string id)
        {
            var models = _modelsRepository.GetItemsList();

            var categorizedModels = from m in models
                                    where m.Category.Name == id
                                    select m;
            if (categorizedModels.Count() > 0)
            {
                return View(categorizedModels.ToList());
            }
            return null;
        }

        // GET: Categories/Details/5
        public ActionResult Details(int id)
        {
            return View(_categoriesRepository.GetItem(id));
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryID,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                _categoriesRepository.Create(category);
                _categoriesRepository.Save();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int id)
        {     
            return View(_categoriesRepository.GetItem(id));
        }
        
        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryID,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                _categoriesRepository.Update(category);
                _categoriesRepository.Save();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_categoriesRepository.GetItem(id));
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _categoriesRepository.Delete(id);
            _categoriesRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _categoriesRepository.Dispose();
            _modelsRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}
