﻿using Microsoft.AspNetCore.Mvc;

namespace StockManagementProject.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
