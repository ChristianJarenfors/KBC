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
            string tmpPassword = Request["passwordInput"];
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

            if (tmpUsername.Length < 6)
            {

                return Redirect("User/UsernameTooShort");
            }
            else if (tmpPassword.Length < 6)
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


            if (tmpUsername.Length >= 6 && tmpPassword.Length >= 6)
            {
                User userToAdd = new User(tmpUsername, tmpPassword, tmpEmail);
                Session["UserLoggedIn"] = true;
                Session["CurrentUser"] = tmpUsername;

                context.Users.Add(userToAdd);
                context.SaveChanges();


                return Redirect("/User/RegisterSuccess");
            }


            return Redirect("User/RegisterError");
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