using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KBC.Models
{
    public class Serie
    {
        [Key]
        public virtual int SerieId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Creator { get; set; }
        public virtual DateTime ReleaseDatum { get; set; }

        //[InverseProperty("GenreId")]
        //[ForeignKey("GenreTypes")]
        //public virtual IList<int> GenreIds { get; set; }

        //public virtual IList<Genre> GenreTypes { get; set; }
        
        public virtual ICollection<SerieGenre> Genres { get; set; }
        public virtual string Description { get; set; }
        public virtual int NumberOfVotes { get; set; }
        public virtual float AverageGrade
        {
            get;
            set;
        }
        
        //[ForeignKey("SerieImgsURL")]
        //public virtual IList<int> SerieImgURLIds { get; set; }
        public virtual IList<SerieImgURL> SerieImgsURL { get; set; }
        //[ForeignKey("SerieVideoURL")]
        //public virtual IList<int> SerieVideoURLIds { get; set; }
        //public virtual IList<SerieVideoURL> SerieVideoURL { get; set; }
        

        
    }
}