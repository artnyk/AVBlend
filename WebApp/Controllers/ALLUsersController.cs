using System.Web.Mvc;
using WebApp.Filters;
using WebApp.Models;
using WebApp.Repository;

namespace WebApp.Controllers
{
    //Controller returns list of all registred users
    public class ALLUsersController : Controller
    {
        IRepository<ApplicationUser> _identityUsersRepository;

        public ALLUsersController()
        {
            _identityUsersRepository = new IdentityUsersRepository();
        }
        
        [Culture]
        public ActionResult Index()
        {
            return View(_identityUsersRepository.GetItemsList());
        }
        
    }
}
