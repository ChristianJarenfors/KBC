using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KBC.Models
{
    public class SerieVideoURL
    {
        public virtual int Id { get; set; }
        public virtual int SerieId { get; set; }
        public virtual string VideoURL { get; set; }
    }
}