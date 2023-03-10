using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JokesApp.Data;
using JokesApp.Models;

namespace JokesApp.Controllers
{
    public class JokeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JokeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Joke
        public async Task<IActionResult> Index()
        {
              return View(await _context.JokeModel.ToListAsync());
        }
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // GET: Joke/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.JokeModel == null)
            {
                return NotFound();
            }

            var jokeModel = await _context.JokeModel
                .FirstOrDefaultAsync(m => m.ID == id);
            if (jokeModel == null)
            {
                return NotFound();
            }

            return View(jokeModel);
        }

        // GET: Joke/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Joke/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,JokeAnswer,JokeQuestion")] JokeModel jokeModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jokeModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jokeModel);
        }

        // GET: Joke/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.JokeModel == null)
            {
                return NotFound();
            }

            var jokeModel = await _context.JokeModel.FindAsync(id);
            if (jokeModel == null)
            {
                return NotFound();
            }
            return View(jokeModel);
        }

        // POST: Joke/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,JokeAnswer,JokeQuestion")] JokeModel jokeModel)
        {
            if (id != jokeModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jokeModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JokeModelExists(jokeModel.ID))
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
            return View(jokeModel);
        }

        // GET: Joke/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.JokeModel == null)
            {
                return NotFound();
            }

            var jokeModel = await _context.JokeModel
                .FirstOrDefaultAsync(m => m.ID == id);
            if (jokeModel == null)
            {
                return NotFound();
            }

            return View(jokeModel);
        }

        // POST: Joke/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.JokeModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.JokeModel'  is null.");
            }
            var jokeModel = await _context.JokeModel.FindAsync(id);
            if (jokeModel != null)
            {
                _context.JokeModel.Remove(jokeModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JokeModelExists(int id)
        {
          return _context.JokeModel.Any(e => e.ID == id);
        }
    }
}
