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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly DataContext _dbContext;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, DataContext dataContext)
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
        
        [HttpPost]
        public IActionResult Authorization(string name, string email, string passowrd,string group, string leader)
        {
            using (DataContext db = _dbContext)
            {
                bool led;
                MD5 md5 = MD5.Create();
                byte[] passBytes = Encoding.ASCII.GetBytes(passowrd);// преобразуем строку в массив байтов
                byte[] hash = md5.ComputeHash(passBytes); //получаем хэш в виде массива байтов
                string heshpasswd = Convert.ToHexString(hash); // преобразуем хеш в строку
                if (leader == "да" || leader == "Да")
                    led = true;
                else
                    led = false;
                User usr = new User 
                { 
                    id = Guid.NewGuid(), 
                    name = name, 
                    email = email, 
                    password = heshpasswd, 
                    group = group, 
                    leader = led
                };
                db.User.Add(usr);
                db.SaveChanges();
            }
            return View("Sucsess!");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}