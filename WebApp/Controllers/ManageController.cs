using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using WebApp.Models;
using System.Collections.Generic;
using WebApp.Filters;
using System.IO;
using WebApp.Repository;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    //Controller for manage user account and models
    [Culture]
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        IRepository<User> _usersRepository;
        IRepository<Category> _categoriesRepository;
        IRepository<Model> _modelsRepository;
        ApplicationDbContext _adb = new ApplicationDbContext();


        public ManageController()
        {
            _usersRepository = new UsersRepository();
            _modelsRepository = new ModelsRepository();
            _categoriesRepository = new CategoriesRepository();
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        
        //Account settings
        // GET: /Manage/Index
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var user = (from t in _usersRepository.GetItemsList() where t.IdentityUserId == userId select t).Single();
            var manageViewModel = new ManageViewModel()
            {
                UserID=user.UserID,
                Login = user.Login,
                Email = user.Email,
                Webmoney_Wallet = user.Webmoney_Wallet
            };
            return View(manageViewModel);      
        }
        
        [HttpPost]
        public ActionResult Index(ManageViewModel manageViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _usersRepository.GetItem(manageViewModel.UserID);

                user.Login = manageViewModel.Login;
                user.Email = manageViewModel.Email;
                user.Webmoney_Wallet = manageViewModel.Webmoney_Wallet;
                _usersRepository.Update(user);
                _usersRepository.Save();
                _adb.Users.Find(user.IdentityUserId).UserName = user.Login;
                _adb.Users.Find(user.IdentityUserId).Email = user.Email;
                _adb.SaveChanges();
             
                return RedirectToAction("Index");
            }
            return View(manageViewModel);
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //List of users models
        public ActionResult MyModels()
        {
            var identityUserId = User.Identity.GetUserId();
            User user = new User();
            using (ModelsRepository db = new ModelsRepository())
            {
                var myModels = (from t in db.GetItemsList() where t.User.IdentityUserId == identityUserId select t).ToList();
                return View(myModels);
            }    
        }

        //----------------------------------------------
        //List of users orders (NOT IMPLEMENTED)
        public ActionResult MyOrders()
        {
            List<Order> myOrders = new List<Order>();
            var userName = User.Identity.Name;
            User user = new User();
            using (DatabaseContext db = new DatabaseContext())
            {
                foreach (var item in db.Users)
                {
                    if (item.Login == userName)
                    {
                        user = item;
                    }
                }

                foreach (var item in db.Orders)
                {
                    if (item.UserID == user.UserID)
                    {
                        myOrders.Add(item);
                    }
                }
            }
            return View(myOrders);
        }
        //----------------------------------------------

        public ActionResult Model(int? id)
        {          
            return View(_modelsRepository.GetItem((int)id));
        }

        //Upload new model
        //GET
        public ActionResult UploadModel()
        {
            var categories = _categoriesRepository.GetItemsList();
            var uploadViewModel = new UploadViewModel()
            {
                Catigories = new SelectList(categories, "CategoryID", "Name")
            };
            return View(uploadViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadModel(UploadViewModel uploadViewModel, List<HttpPostedFileBase> uploads)
        {
            var user = (from t in _usersRepository.GetItemsList() where t.IdentityUserId == User.Identity.GetUserId() select t).Single();
            var model = new Model()
            {
                CategoryID = uploadViewModel.CategoryID,
                Name = uploadViewModel.Name,
                Price = uploadViewModel.Price,
                DateCreation = DateTime.Now,
                Enabled = true,
                SoldNum = 0,
                UserID = user.UserID,
            };
            _modelsRepository.Create(model);
            _modelsRepository.Save();

            Directory.CreateDirectory(Server.MapPath("~/Files/model-" + model.ModelID));
            foreach (var file in uploads)
            {
                if (file != null)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    file.SaveAs(Server.MapPath("~/Files/" + "model-" + model.ModelID + "/" + fileName));
                }
            }

            uploadViewModel.Catigories = new SelectList(_categoriesRepository.GetItemsList(), "CategoryID", "Name");
            return Redirect("MyModels");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }
            _usersRepository.Dispose();
            base.Dispose(disposing);
        }

#region Вспомогательные приложения
        // Используется для защиты от XSRF-атак при добавлении внешних имен входа
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

#endregion
    }
}