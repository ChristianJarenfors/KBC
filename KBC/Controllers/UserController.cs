using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KBC.Models;
using System.Net.Mail;

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

            UserContext context = new UserContext();
            List<User> users = new List<User>();
            string tmpUsername = Request["usernameInput"];
            string tmpEmail = Request["emailInput"];
            string tmpPassword = Request["passwordInput"];
            string tmpPasswordRetype = Request["passwordInputRetype"];

            if (string.IsNullOrWhiteSpace(tmpUsername) || string.IsNullOrWhiteSpace(tmpEmail) || string.IsNullOrWhiteSpace(tmpPassword))
            {

                return Redirect("/User/FieldIsEmpty");
            }


            foreach (var user in context.Users.AsEnumerable())
            {
                users.Add(user);

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


            if (tmpUsername.Length >= 6 && tmpPassword.Length >= 6)
            {
                User userToAdd = new User(tmpUsername, tmpPassword, tmpEmail);

                context.Users.Add(userToAdd);
                context.SaveChanges();


                return Redirect("/User/Register Success");
            }


            return Redirect("User/RegisterError");
        }

    }
}