using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KBC.Models
{
    public class Genre
    {
        
        [Key]
        public virtual int Id { get; set; }
        public virtual GenreCollection GenreType { get; set; }
        [ForeignKey("Serie")]
        public virtual IList<int> SerieId { get; set; }
        public virtual IList<Serie> Serie { get; set; }

    }
}