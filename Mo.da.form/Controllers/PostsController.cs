using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MO.DA.FORM.Models;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Text;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace MO.DA.FORM.Controllers
{
    

    public class PostsController : Controller
    {
        private readonly DataContext _dbContext;

        public PostsController(DataContext context)
        {
            _dbContext = context;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            return _dbContext.Post != null ?
                        View(await _dbContext.Post.ToListAsync()) :
                        Problem("Entity set 'DataContext.Post'  is null.");
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _dbContext.Post == null)
            {
                return NotFound();
            }

            var post = await _dbContext.Post
                .FirstOrDefaultAsync(m => m.post_id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create_post_new
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("post_id,type,text,datetime,file")] PostViewModel pwm)
        {
            if (ModelState.IsValid)
            {
                Post post = new Post() { post_id = pwm.post_id, text = pwm.text, type = pwm.type };
                post.datetime = $"{pwm.datetime.Day}.{pwm.datetime.Month}.{pwm.datetime.Year}";
                if (pwm.file != null)
                {
                    byte[] imageData = null;
                    // считываем переданный файл в массив байтов
                    using (var binaryReader = new BinaryReader(pwm.file.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)pwm.file.Length);
                    }
                    // установка массива байтов
                    post.file = imageData;
                }
                else { post.file = null; }


                _dbContext.Add(post);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pwm);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _dbContext.Post == null)
            {
                return NotFound();
            }

            var post = await _dbContext.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("post_id,type,text,datetime,file")] Post post)
        {
            if (id != post.post_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Update(post);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.post_id))
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
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _dbContext.Post == null)
            {
                return NotFound();
            }

            var post = await _dbContext.Post
                .FirstOrDefaultAsync(m => m.post_id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_dbContext.Post == null)
            {
                return Problem("Entity set 'DataContext.Post'  is null.");
            }
            var post = await _dbContext.Post.FindAsync(id);
            if (post != null)
            {
                _dbContext.Post.Remove(post);

            }

            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return (_dbContext.Post?.Any(e => e.post_id == id)).GetValueOrDefault();
        }

        
        // GET: Posts
        public async Task<IActionResult> Index_homework()
        {
            return _dbContext.Post != null ?
                        View(await _dbContext.Post.ToListAsync()) :
                        Problem("Entity set 'DataContext.Post'  is null.");
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details_homework(int? id)
        {
            if (id == null || _dbContext.Post == null)
            {
                return NotFound();
            }

            var post_homework = await _dbContext.Post
                .FirstOrDefaultAsync(m => m.post_id == id);
            if (post_homework == null)
            {
                return NotFound();
            }

            return View(post_homework);
        }
        
        // GET: Posts/Create_post_new
        public IActionResult Create_homework()
        {
            return View();
        }

        // POST: Posts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create_homework([Bind("post_id,type,text,datetime,file")] PostViewModel pwm)
        {
            if (ModelState.IsValid)
            {
                Post post_homework = new Post() { post_id = pwm.post_id, text = pwm.text, type = pwm.type };
                post_homework.datetime = $"{pwm.datetime.Day}.{pwm.datetime.Month}.{pwm.datetime.Year}";
                if (pwm.file != null)
                {
                    byte[] imageData = null;
                    // считываем переданный файл в массив байтов
                    using (var binaryReader = new BinaryReader(pwm.file.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)pwm.file.Length);
                    }
                    // установка массива байтов
                    post_homework.file = imageData;
                }
                else { post_homework.file = null; }


                _dbContext.Add(post_homework);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pwm);
        }
    }
}