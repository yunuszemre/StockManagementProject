﻿using Microsoft.AspNetCore.Mvc;

namespace StockManagementProject.UI.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
