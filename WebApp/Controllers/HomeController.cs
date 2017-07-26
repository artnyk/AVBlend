using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using WebApp.Filters;
using WebApp.Models;
using WebApp.Repository;
namespace WebApp.Controllers
{
    //Main page controller
    [Culture]
    public class HomeController : Controller
    {
        IRepository<Category> _categoriesRepository;
        IRepository<Model> _modelsRepository;

        public HomeController()
        {
            _modelsRepository = new ModelsRepository();
            _categoriesRepository = new CategoriesRepository();
        }

        public ActionResult Index()
        {
            ViewBag.UploadedModels = _modelsRepository.GetItemsList();
            return View(_categoriesRepository.GetItemsList());
        }

        //Action Method for change  site language
        public ActionResult ChangeCulture(string lang)
        {
            string returnUrl = Request.UrlReferrer.AbsolutePath;
            // Cultures List
            List<string> cultures = new List<string>() { "uk", "en" };
            if (!cultures.Contains(lang))
            {
                lang = "en";
            }
            // Save culture to cookie
            HttpCookie cookie = Request.Cookies["lang"];
            if (cookie != null)
                cookie.Value = lang;   // if cookie has value - update
            else
            {
                cookie = new HttpCookie("lang");
                cookie.HttpOnly = false;
                cookie.Value = lang;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return Redirect(returnUrl);
        }

        public ActionResult Help()
        {
            return View("Help");
        }

        protected override void Dispose(bool disposing)
        {
            _categoriesRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}