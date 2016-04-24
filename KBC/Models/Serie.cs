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
        public virtual int Id { get; set; }
        public virtual String Name { get; set; }
        public virtual DateTime ReleaseDatum { get; set; }
        [ForeignKey("GenreTypes")]
        public virtual IList<int> GenreId { get; set; }
        public virtual IList<Genre> GenreTypes { get; set; }
        public virtual int NumberOfVotes { get; set; }
        public virtual float AverageGrade
        {
            get;
            set;
        }
        public virtual string Description { get; set; }
        [ForeignKey("SerieImgsURL")]
        public virtual IList<int> SerieImgURLIds { get; set; }
        public virtual IList<SerieImgURL> SerieImgsURL { get; set; }
        [ForeignKey("SerieVideoURL")]
        public virtual IList<int> SerieVideoURLIds { get; set; }
        public virtual IList<SerieVideoURL> SerieVideoURL { get; set; }
        public Serie() {
            GenreId = null;
            GenreTypes = null;
            SerieImgsURL = null;
            SerieVideoURL = null;
            
        }

        //if (NumberOfVotes!=0)
        //{
        //    float oldscore = NumberOfVotes * AverageGrade;
        //    oldscore += value;
        //    NumberOfVotes=NumberOfVotes+1;
        //    AverageGrade = oldscore / NumberOfVotes;
        //}
        //else
        //{
        //    AverageGrade = 0;
        //}
    }
}