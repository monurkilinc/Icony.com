﻿using Microsoft.AspNetCore.Mvc;

namespace Icony.Web.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
