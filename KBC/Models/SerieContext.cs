using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KBC.Models
{
    public class SerieContext:DbContext
    {
        public SerieContext() : base() { }
        public DbSet<Serie> Serie { get; set; }
        public DbSet<Genre> Genre { get; set; }
    }
}