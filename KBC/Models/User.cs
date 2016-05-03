using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KBC.Models
{
    public class User
    {

        public User()
        {

        }

        public User(string username, string password, string email)
        {
            this.Username = username;
            this.Password = password;
            this.Email = email;

        }

        public virtual int Id { get; set; }
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        public virtual string Email { get; set; }
        public virtual string ProfilePictureUrl { get; set; }
        public virtual int Age { get; set; }
        public virtual string Country { get; set; }
        public virtual string Gender { get; set; }
        public virtual bool isAdmin { get; set; }
        public virtual IList<Serie> SeriesFollowed { get; set; }


    }
}