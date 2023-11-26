using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MO.DA.FORM.Models;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Text;
using System;

namespace MO.DA.FORM.Controllers
{
    public class NewsController : Controller
    {
        private readonly ILogger<NewsController> _logger;
        private readonly IConfiguration _configuration;
        private readonly DataContext _dbContext;
        public NewsController(ILogger<NewsController> logger, IConfiguration configuration, DataContext dataContext)
        {
            _logger = logger;
            _configuration = configuration;
            _dbContext = dataContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Auth()
        {
            return View();
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult List_of_news()
        {
            return View("~/Views/News/List_of_news.cshtml");
        }
        public IActionResult New()
        {
            return View("~/Views/News/New.cshtml");
        }
    }
}