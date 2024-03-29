﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MO.DA.FORM.Models;

namespace MO.DA.FORM.Controllers
{
    public class HomeworkController : Controller
    {
        private readonly DataContext _context;

        public HomeworkController(DataContext context)
        {
            _context = context;
        }



        // GET: Homework/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Homework == null)
            {
                return NotFound();
            }

            var homework = await _context.Homework
                .FirstOrDefaultAsync(m => m.id == id);
            if (homework == null)
            {
                return NotFound();
            }

            return View(homework);
        }

        // GET: Homework/Create
        [Authorize(Policy = "LeaderLimit")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Homework/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,deadline,text,subject")] HomeworkViewModel homework)
        {
            if (ModelState.IsValid)
            {
                Homework work = new Homework() { id = homework.id, text = homework.text, subject = homework.subject };
                work.deadline = $"{homework.deadline.Day}.{homework.deadline.Month}.{homework.deadline.Year}";
                _context.Add(work);
                await _context.SaveChangesAsync();
                return RedirectToAction("Student", "Home");
            }
            return View(homework);
        }

        // GET: Homework/Edit/5
        [Authorize(Policy = "LeaderLimit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Homework == null)
            {
                return NotFound();
            }

            var homework = await _context.Homework.FindAsync(id);
            if (homework == null)
            {
                return NotFound();
            }
            return View(homework);
        }

        // POST: Homework/Edit/5
        [HttpPost]
        [Authorize(Policy = "LeaderLimit")]
        public async Task<IActionResult> Edit(int id, [Bind("id,deadline,text,subject")] Homework homework)
        {
            if (id != homework.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(homework);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomeworkExists(homework.id))
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
            return Redirect("~/Home/Student");
        }

        // GET: Homework/Delete/5
        [Authorize(Policy = "LeaderLimit")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Homework == null)
            {
                return NotFound();
            }

            var homework = await _context.Homework
                .FirstOrDefaultAsync(m => m.id == id);
            if (homework == null)
            {
                return NotFound();
            }

            return View(homework);
        }

        // POST: Homework/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "LeaderLimit")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Homework == null)
            {
                return Problem("Entity set 'DataContext.Homework'  is null.");
            }
            var homework = await _context.Homework.FindAsync(id);
            if (homework != null)
            {
                _context.Homework.Remove(homework);
            }

            await _context.SaveChangesAsync();
            return Redirect("~/Home/Student");
        }

        private bool HomeworkExists(int id)
        {
            return (_context.Homework?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
