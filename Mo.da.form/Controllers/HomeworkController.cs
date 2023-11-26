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
    public class HomeworkController : Controller
    {
        private readonly ILogger<HomeworkController> _logger;
        private readonly IConfiguration _configuration;
        private readonly DataContext _dbContext;
        public HomeworkController(ILogger<HomeworkController> logger, IConfiguration configuration, DataContext dataContext)
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
        public IActionResult Homework_daily()
        {
            return View("~/Views/Homework/Homework_daily.cshtml");
        }
        public IActionResult Homework_semestr()
        {
            return View("~/Views/Homework/Homework_semestr.cshtml");
        }
        public IActionResult Deadline_daily()
        {
            return View("~/Views/Homework/Deadline_daily.cshtml");
        }
        public IActionResult Deadline_semestr()
        {
            return View("~/Views/Homework/Deadline_semestr.cshtml");
        }
        public IActionResult Homework()
        {
            return View("~/Views/Homework/Homework.cshtml");
        }
        public IActionResult List_of_quest()
        {
            return View("~/Views/Homework/List_of_quest.cshtml");
        }
    }
}