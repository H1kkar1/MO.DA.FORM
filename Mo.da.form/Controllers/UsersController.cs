using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MO.DA.FORM.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;

namespace MO.DA.FORM.Controllers
{
    public class UsersController : Controller
    {
        private readonly DataContext _context;
        public MD5 md5 = MD5.Create();
        private readonly Regex mailreg = new Regex(@"\w+@");
        private readonly Regex groupreg = new Regex(@"2\d{2}-\d");

        //Подключение бд
        public UsersController(DataContext context)
        {
            _context = context;
        }

        //Хеширование пароля
        public string get_hash_paswd(string password)
        {
            byte[] passBytes = Encoding.ASCII.GetBytes(password);// преобразуем строку в массив байтов
            byte[] hash = md5.ComputeHash(passBytes); //получаем хэш в виде массива байтов
            string heshpasswd = Convert.ToHexString(hash); // преобразуем хеш в строку

            return heshpasswd;
        }
        
        //Проварка через регулярные выражения
        public bool RegexValidation(UserViewModel user)
        {
            if (!(mailreg.Matches(user.email).Count > 0))
            {
                return false;
            }
            if (!(groupreg.Matches(user.group).Count > 0))
            {
                return false;
            }

            return true;


        }

        // GET: Users/Details/5 (ПРОФИЛЬ)
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create    (РЕГИСТРЦИЯ)
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create   (РЕГИСТРАЦИЯ)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,email,password,proverka_password,group,leader")] UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                if (!(RegexValidation(user))) { ModelState.AddModelError("email", "Неправельно введён Email или группа"); }

                else if(user.proverka_password == user.password)
                {
                    User user1 = new User() { email = user.email, password = user.password, group = user.group, id = user.id, leader = user.leader, name = user.name };
                    user1.password = get_hash_paswd(user1.password);
                    _context.Add(user1);
                    await _context.SaveChangesAsync();
                    await Authenticate(user1);
                    return Redirect("~/Home/Student");
                }
                else
                {
                    ModelState.AddModelError("password", "Пароли не совпадают");
                }
                
            }
            return View(user);
        }

        // GET: Users/Edit/5    (РЕАДКТИРОВАНИЕ ПРОФИЛЯ)
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5    (РЕАДКТИРОВАНИЕ ПРОФИЛЯ)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("id,name,email,password,group,leader")] User user)
        {
            if (id != user.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    user.password = get_hash_paswd(user.password);
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5  (УДАЛЕНИЕ АККАУНТА)
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.User == null)
            {
                return Problem("Entity set 'DataContext.User'  is null.");
            }
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Response.Cookies.Delete("leader");
            return RedirectToAction("Lending","Home");
        }

        private bool UserExists(Guid id)
        {
          return (_context.User?.Any(e => e.id == id)).GetValueOrDefault();
        }
        //GET: Users/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.User.FirstOrDefaultAsync(u => u.email == model.email && u.password == get_hash_paswd(model.password));
                if (user != null)
                {
                    await Authenticate(user);
                }
                else
                {
                    ModelState.AddModelError("Email", "Неправельно введён email или такого пользователя нет");
                    return View(model);
                }
                
            }
            return Redirect("~/Home/Student");
            
        }
        [ValidateAntiForgeryToken]
        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.id.ToString()),
                new Claim("leader", user.leader.ToString()),             
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims,"UserCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id), new AuthenticationProperties
            {
                IsPersistent = true
            });

        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Users");
        }
    }
}
