using Microsoft.AspNetCore.Mvc;
using MO.DA.FORM.Models;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MO.DA.FORM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
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
        public IActionResult Authorization(string name, string email, string passowrd,string group)
        {
            using (DataContext db = new DataContext(_configuration))
            {
                User usr = new User { name = name, email = email, password = passowrd, group = group };
                db.Users.Add(usr);
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