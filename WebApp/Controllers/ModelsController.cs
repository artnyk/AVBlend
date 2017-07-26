using System.Web.Mvc;
using WebApp.Filters;
using WebApp.Models;
using WebApp.Repository;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    //Controller for managing models
    [Culture]
    public class ModelsController : Controller
    {
        IRepository<Category> _categoriesRepository;
        IRepository<Model> _modelsRepository;
 
        public ModelsController()
        {
            _categoriesRepository = new CategoriesRepository();
            _modelsRepository = new ModelsRepository();
        }

        // GET: Models list
        public ActionResult Index()
        {
            return View(_modelsRepository.GetItemsList());
        }

        // GET: Models/Details/5
        public ActionResult Details(int id)
        {
            var model = _modelsRepository.GetItem(id);
            var modelDetailsViewModel = new ModelDetailsViewModel()
            {
                ModelID = model.ModelID,
                CategoryName = model.Category.Name,
                DateCreation = model.DateCreation,
                Name = model.Name,
                Price = model.Price,
                SellerName = model.User.Login,
                SoldNum = model.SoldNum
            };
            return View(modelDetailsViewModel);
        }

        //Download model file
        [HttpPost]
        public FileResult GetFile(int id)
        {
            var model = _modelsRepository.GetItem(id);

            string file_path = Server.MapPath("~/Files/model-" + model.ModelID + "/" + model.Name + ".blend");
            string file_type = "application/blend";
            string file_name = model.Name + ".blend";

            return File(file_path, file_type, file_name);
        }

        // GET: Models/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            var model = _modelsRepository.GetItem(id);
            var categories = _categoriesRepository.GetItemsList();

            var modelEditViewModel = new ModelEditViewModel()
            {
                ModelID = model.ModelID,
                Name = model.Name,
                Price = model.Price,
                CategoryID = model.CategoryID,
                Catigories = new SelectList(categories, "CategoryID", "Name")
            };
            return View(modelEditViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ModelEditViewModel modelEditViewModel)
        {
            modelEditViewModel.Catigories = new SelectList(_categoriesRepository.GetItemsList(), "CategoryID", "Name");
            if (ModelState.IsValid)
            {
                var model = _modelsRepository.GetItem(modelEditViewModel.ModelID);
                model.Name = modelEditViewModel.Name;
                model.Price = modelEditViewModel.Price;
                model.CategoryID = modelEditViewModel.CategoryID;
                _modelsRepository.Update(model);
                _modelsRepository.Save();

                return RedirectToAction("MyModels");
            }
            return View(modelEditViewModel);
        }

        // GET: Models/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_modelsRepository.GetItem(id));
        }

        // POST: Models/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _modelsRepository.Delete(id);
            _modelsRepository.Save();
            return RedirectToAction("Index");
        }

        // GET: Models/All
        public ActionResult All()
        {
            return View(_modelsRepository.GetItemsList());
        }

        protected override void Dispose(bool disposing)
        {
            _modelsRepository.Dispose();
            _categoriesRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}
