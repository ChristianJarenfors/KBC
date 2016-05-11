using KBC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KBC.Controllers
{
    public class UserProfileController : Controller
    {
        // GET: UserProfile
        public ActionResult Index()
        {
            string userName = (string)Session["CurrentUser"];
            User user = new User();
            if (userName != "John Doe")
            {
                SerieContext context = new SerieContext();
                user = context.Users.Where(u => u.Username == userName).First();
            }
            return View(user);
        }


    }
}