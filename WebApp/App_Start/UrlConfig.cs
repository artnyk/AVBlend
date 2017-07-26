using System.Net;
using System.Web.Mvc;

namespace WebApp.App_Start
{
    public static class UrlConfig
    {
        public static ActionResult CheckId(int? id) 
      {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return null;
      }
    }
}
