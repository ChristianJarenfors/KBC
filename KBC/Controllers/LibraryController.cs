using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KBC.Models;

namespace KBC.Controllers
{
    public class LibraryController : Controller
    {
        // GET: Library
        public ActionResult Index()
        {
            SerieContext SC = new SerieContext();

            List<Serie> List = SC.Serie.ToList();

            return View(List);
        }
    }
}