using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using KBC.Models;

namespace KBC
{
    public class UserContext : DbContext
    {

        public UserContext() : base("Test1")
        {

        }

        public DbSet<User> Users { get; set; }

    }
}