using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DigiLib.Data;
using DigiLib.Models;

namespace DigiLib.Controllers
{
    public class UrlsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UrlsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Urls
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Urls.Include(u => u.Subject);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Urls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var url = await _context.Urls
                .Include(u => u.Subject)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (url == null)
            {
                return NotFound();
            }

            return View(url);
        }

        // GET: Urls/Create
        public IActionResult Create()
        {
            ViewData["SubjectID"] = new SelectList(_context.Subjects, "ID", "ID");
            return View();
        }

        // POST: Urls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UrlName,UrlDescription,Urls,SubjectID")] Url url)
        {
            if (ModelState.IsValid)
            {
                _context.Add(url);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubjectID"] = new SelectList(_context.Subjects, "ID", "ID", url.SubjectID);
            return View(url);
        }

        // GET: Urls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var url = await _context.Urls.SingleOrDefaultAsync(m => m.ID == id);
            if (url == null)
            {
                return NotFound();
            }
            ViewData["SubjectID"] = new SelectList(_context.Subjects, "ID", "ID", url.SubjectID);
            return View(url);
        }

        // POST: Urls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UrlName,UrlDescription,Urls,SubjectID")] Url url)
        {
            if (id != url.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(url);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UrlExists(url.ID))
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
            ViewData["SubjectID"] = new SelectList(_context.Subjects, "ID", "ID", url.SubjectID);
            return View(url);
        }

        // GET: Urls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var url = await _context.Urls
                .Include(u => u.Subject)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (url == null)
            {
                return NotFound();
            }

            return View(url);
        }

        // POST: Urls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var url = await _context.Urls.SingleOrDefaultAsync(m => m.ID == id);
            _context.Urls.Remove(url);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UrlExists(int id)
        {
            return _context.Urls.Any(e => e.ID == id);
        }
    }
}
