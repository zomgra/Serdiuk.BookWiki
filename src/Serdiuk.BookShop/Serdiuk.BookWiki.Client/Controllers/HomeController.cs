﻿using Microsoft.AspNetCore.Mvc;
using Serdiuk.BookWiki.Client.Models;
using System.Diagnostics;

namespace Serdiuk.BookWiki.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Books()
        {
            return View();
        }
    }
}