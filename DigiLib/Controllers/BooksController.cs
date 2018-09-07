using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DigiLib.Data;
using DigiLib.Models;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace DigiLib.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnv;
        public BooksController(ApplicationDbContext context, IHostingEnvironment hEnv)
        {
            _context = context;
            _hostingEnv = hEnv;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Books.Include(b => b.Subject);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Subject)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {

            ViewData["SubjectID"] = new SelectList(_context.Subjects, "ID", "ID");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Books,BookLink,BookImage,SubjectID")] Book book, IList<IFormFile> _files, IList<IFormFile> _ifiles)
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
                    relativeName = "/Subject/Books/" + DateTime.Now.ToString("ddMMyyyy-HHmmssffffff") + fileName;

                    using (FileStream fs = System.IO.File.Create(_hostingEnv.WebRootPath + relativeName))
                    {
                        await file.CopyToAsync(fs);
                        fs.Flush();
                    }
                }
            }
            book.BookLink = relativeName;
            //Image
            var irelativeName = "";
            var ifileName = "";

            if (_ifiles.Count < 1)
            {
                irelativeName = "/Images/Default.jpg";
            }
            else
            {
                foreach (var file in _ifiles)
                {
                    ifileName = ContentDispositionHeaderValue
                                      .Parse(file.ContentDisposition)
                                      .FileName
                                      .Trim('"');
                    //Path for localhost
                    ifileName = Path.GetExtension(ifileName);
                    irelativeName = "/Images/" + DateTime.Now.ToString("ddMMyyyy-HHmmssffffff") + ifileName;

                    using (FileStream fs = System.IO.File.Create(_hostingEnv.WebRootPath + irelativeName))
                    {
                        await file.CopyToAsync(fs);
                        fs.Flush();
                    }
                }
            }
            book.BookImage = irelativeName;

            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubjectID"] = new SelectList(_context.Subjects, "ID", "ID", book.SubjectID);
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.SingleOrDefaultAsync(m => m.ID == id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["SubjectID"] = new SelectList(_context.Subjects, "ID", "ID", book.SubjectID);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Books,BookLink,BookImage,SubjectID")] Book book)
        {
            if (id != book.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.ID))
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
            ViewData["SubjectID"] = new SelectList(_context.Subjects, "ID", "ID", book.SubjectID);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Subject)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.SingleOrDefaultAsync(m => m.ID == id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.ID == id);
        }
    }
}
