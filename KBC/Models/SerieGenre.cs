using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KBC.Models
{
    public class SerieGenre
    {
        public int Id { get; set; }

        public GenreType Genre { get; set; }

        public int SerieId { get; set; }
        public virtual Serie Serie { get; set; }
    }
}