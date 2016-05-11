using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KBC.Models;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace KBC.Controllers
{
    public class UserController : Controller
    {

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Register()
        {

            SerieContext context = new SerieContext();
            List<User> users = new List<User>();
            string tmpUsername = Request["usernameInput"];
            string tmpEmail = Request["emailInput"];
            int tmpAge;
            int.TryParse(Request["ageInput"],out tmpAge);
            string tmpPassword = Request["passwordInput"];
            DateTime tmpBirthday = DateTime.Parse(Request["birthdayInput"]);
            string tmpPasswordRetype = Request["passwordInputRetype"];
            string checkChars = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";


            

            if (string.IsNullOrWhiteSpace(tmpUsername) || string.IsNullOrWhiteSpace(tmpEmail) || string.IsNullOrWhiteSpace(tmpPassword))
            {

                return Redirect("/User/FieldIsEmpty");
            }

            if (context.Users.Count() > 0)
            {

                foreach (var user in context.Users.AsEnumerable())
                {
                    users.Add(user);

                }

            }

            
            if (tmpPassword != tmpPasswordRetype)
            {

                return Redirect("/User/PasswordNotMatch");
            }

            foreach (var user in users.AsEnumerable())
            {
                
                if (tmpUsername.ToLower() == user.Username.ToLower())
                {

                    return Redirect("/User/UsernameExists");
                }

                if (tmpEmail.ToLower() == user.Email.ToLower())
                {

                    return Redirect("/User/EmailExists");
                }

            }

            if (tmpUsername.Trim().Length < 3)
            {

                return Redirect("User/UsernameTooShort");
            }
            else if (tmpPassword.Trim().Length < 6)
            {

                return Redirect("User/PasswordTooShort");
            }

            try
            {
                var addr = new MailAddress(tmpEmail);

            }
            catch
            {
                return Redirect("/User/EmailNotValid");

            }

            if (!Regex.IsMatch(tmpEmail, checkChars))
            {
                return Redirect("/User/EmailNotValid");

            }

            if (tmpBirthday > DateTime.Now)
            {

                return Redirect("/User/NotBornYet");
            }


            if (tmpUsername.Trim().Length >= 3 && tmpPassword.Trim().Length >= 6)
            {
                DateTime zeroTime = new DateTime(1, 1, 1);

                DateTime a = tmpBirthday;
                DateTime b = DateTime.Now;

                TimeSpan span = b - a;
                // because we start at year 1 for the Gregorian 
                // calendar, we must subtract a year here.
                int years = (zeroTime + span).Year - 1;
				
				User userToAdd = new User(tmpUsername, tmpPassword, tmpEmail, years);

                Session["UserLoggedIn"] = true;
                Session["CurrentUser"] = tmpUsername;

                context.Users.Add(userToAdd);
                context.SaveChanges();


                return Redirect("/User/RegisterSuccess");
            }


            return Redirect("/User/RegisterError");
        }


        public ActionResult Login()
        {

            SerieContext context = new SerieContext();

            List<User> listOfUsers = new List<User>();


            string tmpUsername = Request["Username"];
            string tmpPassword = Request["Password"];
            bool canLogIn = false;


            if (context.Users.Count() > 0)
            {

                foreach (var user in context.Users.AsEnumerable())
                {

                    if (tmpUsername.ToLower() == user.Username.ToLower())
                    {
                        if (tmpPassword == user.Password)
                        {
                            tmpUsername = user.Username;
                            canLogIn = true;
                            break;

                        }

                    }

                }

            }
            

            if (canLogIn)
            {
                Session["UserLoggedIn"] = true;
                Session["CurrentUser"] = tmpUsername;

            }
            else
            {

                Redirect("/User/WrongLogin");
            }


            return Redirect("/Home/Index");
        }

        public ActionResult Logout()
        {
            /*if ((bool)Session["UserLoggedIn"] == true)
            {
                Session["UserLoggedIn"] = false;
                Session["CurrentUser"] = "";

            }*/

            Session.Abandon();


            return Redirect("/Home/Index");
        }


        public ActionResult EmailExists()
        {

            return View();
        }

        public ActionResult EmailNotValid()
        {

            return View();
        }

        public ActionResult FieldIsEmpty()
        {

            return View();
        }

        public ActionResult PasswordNotMatch()
        {

            return View();
        }

        public ActionResult PasswordTooShort()
        {

            return View();
        }

        public ActionResult RegisterError()
        {

            return View();
        }

        public ActionResult RegisterSuccess()
        {

            return View();
        }

        public ActionResult UsernameExists()
        {

            return View();
        }

        public ActionResult UsernameTooShort()
        {

            return View();
        }


    }
}