﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KBC.Models;

namespace KBC.Controllers
{
    public class SerieProfileController : Controller
    {
        // GET: MovieProfile
        public ActionResult Index(int? id)
        {
            SerieContext SC = new SerieContext();
            if (id!=null)
            {
                
                var Serie = SC.Serie.Where(s => s.SerieId == id).First();
                return View(Serie);
            }
            return View(SC.Serie.First());
        }
    }
}