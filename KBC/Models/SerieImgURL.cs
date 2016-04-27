using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KBC.Models
{
    public class SerieImgURL
    {
        public virtual int Id { get; set; }
        //[ForeignKey("SerieId")]
        public virtual int SerieId { get; set; }
        public virtual ImgType ImgType { get; set; }
        public virtual string ImgURL { get; set; }
    }
}