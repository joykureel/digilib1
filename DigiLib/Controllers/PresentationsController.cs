using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DigiLib.Data;
using DigiLib.Models;
using System.Net.Http.Headers;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace DigiLib.Controllers
{
    public class PresentationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnv;
        public PresentationsController(ApplicationDbContext context, IHostingEnvironment hEnv)
        {
            _context = context;
            _hostingEnv = hEnv;
        }

        // GET: Presentations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Presentations.Include(p => p.Subject);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Presentations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presentation = await _context.Presentations
                .Include(p => p.Subject)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (presentation == null)
            {
                return NotFound();
            }

            return View(presentation);
        }

        // GET: Presentations/Create
        public IActionResult Create()
        {
            ViewData["SubjectID"] = new SelectList(_context.Subjects, "ID", "ID");
            return View();
        }

        // POST: Presentations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,PresentationName,Presentations,SubjectID")] Presentation presentation, IList<IFormFile> _files)
        {
            var relativeName = "";
            var fileName = "";

            if (_files.Count < 1)
            {
                relativeName = "/Images/Default.jpg";
            }
            else
            {
                foreach (var file in _files)
                {
                    fileName = ContentDispositionHeaderValue
                                      .Parse(file.ContentDisposition)
                                      .FileName
                                      .Trim('"');
                    //Path for localhost
                    fileName = Path.GetExtension(fileName);
                    relativeName = "/Subject/Presentation/" + DateTime.Now.ToString("ddMMyyyy-HHmmssffffff") + fileName;

                    using (FileStream fs = System.IO.File.Create(_hostingEnv.WebRootPath + relativeName))
                    {
                        await file.CopyToAsync(fs);
                        fs.Flush();
                    }
                }
            }
            presentation.Presentations = relativeName;
            if (ModelState.IsValid)
            {
                _context.Add(presentation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubjectID"] = new SelectList(_context.Subjects, "ID", "ID", presentation.SubjectID);
            return View(presentation);
        }

        // GET: Presentations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presentation = await _context.Presentations.SingleOrDefaultAsync(m => m.ID == id);
            if (presentation == null)
            {
                return NotFound();
            }
            ViewData["SubjectID"] = new SelectList(_context.Subjects, "ID", "ID", presentation.SubjectID);
            return View(presentation);
        }

        // POST: Presentations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,PresentationName,Presentations,SubjectID")] Presentation presentation)
        {
            if (id != presentation.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(presentation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PresentationExists(presentation.ID))
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
            ViewData["SubjectID"] = new SelectList(_context.Subjects, "ID", "ID", presentation.SubjectID);
            return View(presentation);
        }

        // GET: Presentations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presentation = await _context.Presentations
                .Include(p => p.Subject)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (presentation == null)
            {
                return NotFound();
            }

            return View(presentation);
        }

        // POST: Presentations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var presentation = await _context.Presentations.SingleOrDefaultAsync(m => m.ID == id);
            _context.Presentations.Remove(presentation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PresentationExists(int id)
        {
            return _context.Presentations.Any(e => e.ID == id);
        }
    }
}
