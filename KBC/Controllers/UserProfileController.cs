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

        public ActionResult EditDetails()
        {

            SerieContext context = new SerieContext();

            string sessionUsername = (string)Session["CurrentUser"];
            User currentUser = context.Users.ToList().Where(u => u.Username == sessionUsername).First();
            

            string tmpUsername = Request["username"];
            string tmpLocation = Request["location"];
            string tmpAge = Request["birthday"];
            int age = 0;
            string tmpEmail = Request["email"];


            if (!string.IsNullOrWhiteSpace(tmpUsername))
            {
                if (!context.Users.Any(u => u.Username == tmpUsername))
                {
                    currentUser.Username = tmpUsername;
                    Session["CurrentUser"] = tmpUsername;

                }

            }

            if (!string.IsNullOrWhiteSpace(tmpLocation))
            {
                currentUser.Country = tmpLocation;

            }

            if (!string.IsNullOrWhiteSpace(tmpAge) && int.TryParse(tmpAge, out age))
            {
                currentUser.Age = age;

            }

            if (!string.IsNullOrWhiteSpace(tmpEmail))
            {
                currentUser.Email = tmpEmail;

            }

            context.SaveChanges();



            return RedirectToAction("/Index");

        }


    }
}