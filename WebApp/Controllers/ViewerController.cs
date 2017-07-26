using System.Web.Mvc;
using WebApp.Filters;
using WebApp.Models;
using WebApp.Repository;

namespace WebApp.Controllers
{
    //Controller for models 3D viewer
    [Culture]
    public class ViewerController : Controller
    {
        IRepository<Model> db;
        // GET: Viewer
        public ViewerController()
        {
            db = new ModelsRepository();
        }
        public ActionResult Model(int id)
        { 
            return View(db.GetItem(id));
        }
    }
}