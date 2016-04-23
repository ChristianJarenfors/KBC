using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KBC.Models
{
    public class Genre
    {
        //public virtual int ID { get; set; }
        [Key]
        public virtual GenreCollection GenreType { get; set; }
        public virtual IList<int> SerieId { get; set; }
        public virtual IList<Serie> Serie { get; set; }

    }
}