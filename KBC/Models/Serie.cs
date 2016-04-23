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
        public virtual IList<int> GenreIds { get; set; }
        public virtual IList<Genre> GenreTypes { get; set; }
        public virtual int NumberOfVotes { get; private set; }
        public virtual float AverageGrade
        {
            get
            { return AverageGrade; }
            set
            {
                float oldscore = NumberOfVotes * AverageGrade;
                oldscore += value;
                NumberOfVotes++;
                AverageGrade = AverageGrade / NumberOfVotes;
            }
        }
        public virtual string Description { get; set; }
        public virtual IList<string> SerieImgsURL { get; set; }
        public virtual IList<string> SerieVideoURL { get; set; }
        public Serie() {
            GenreIds = null;
            GenreTypes = null;
            SerieImgsURL = null;
            SerieVideoURL = null;
            NumberOfVotes = 0;
            AverageGrade = 0;
        }


    }
}