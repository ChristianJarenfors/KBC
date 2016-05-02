﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using KBC.Models;

namespace KBC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            SerieContext context = new SerieContext();

            int numberOfUsers = context.Users.Count();

            return View();
        }
    }
}