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
    public class PostsController : Controller
    {
        private readonly ILogger<PostsController> _logger;
        private readonly IConfiguration _configuration;
        private readonly DataContext _dbContext;
        public PostsController(ILogger<PostsController> logger, IConfiguration configuration, DataContext dataContext)
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
        public IActionResult Create_post()
        {
            return View("~/Views/Posts/Create_post.cshtml");
        }
        public IActionResult Create_answer_for_quest()
        {
            return View("~/Views/Posts/Create_answer_for_quest.cshtml");
        }
        public IActionResult Create_homework()
        {
            return View("~/Views/Posts/Create_homework.cshtml");
        }
        public IActionResult Create_treb()
        {
            return View("~/Views/Posts/Create_treb.cshtml");
        }
    }
}