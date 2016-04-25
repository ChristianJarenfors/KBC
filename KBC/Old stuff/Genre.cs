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
        public virtual int GenreId { get; set; }
        public virtual GenreCollection GenreType { get; set; }
        [ForeignKey("Serie")]
        //[InverseProperty("SerieId")]
        public IList<int> SerieIds { get; set; }
        
        public virtual IList<Serie> Serie { get; set; }

    }
}