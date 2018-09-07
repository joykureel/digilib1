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
    public class MemberSubjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MemberSubjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MemberSubjects
        public async Task<IActionResult> Index(int? Bid,int? Pid,int? Vid,int ?Uid)
        {
            var viewModel = new IndexData();
            viewModel.Subjects = await _context.Subjects
                .Include(i => i.Books)
                .Include(i => i.Presentations)
                .Include(i => i.Quizzes)
                .Include(i => i.Urls)
                .Include(i => i.Videos)
                .AsNoTracking()
                .OrderBy(i => i.SubjectName)
                .ToListAsync();
            if(Bid != null)
            {
                ViewData["BookID"] = Bid.Value;
                viewModel.Books = viewModel.Subjects.Where(
                i => i.ID == Bid.Value).Single().Books;

            }
            if (Pid != null)
            {
                ViewData["PresentationID"] = Pid.Value;
                viewModel.Presentations = viewModel.Subjects.Where(
                i => i.ID == Pid).Single().Presentations;

            }
            if (Vid != null)
            {
                ViewData["VideoID"] = Vid.Value;
                viewModel.Videos = viewModel.Subjects.Where(
                i => i.ID == Vid).Single().Videos;

            }
            if (Uid != null)
            {
                ViewData["UrlID"] = Uid.Value;
                viewModel.Urls = viewModel.Subjects.Where(
                i => i.ID == Uid).Single().Urls;

            }


            return View(viewModel);
        }


        // GET: MemberSubjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.Subjects
                .SingleOrDefaultAsync(m => m.ID == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // GET: MemberSubjects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MemberSubjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,SubjectName,Description")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subject);
        }

        // GET: MemberSubjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.Subjects.SingleOrDefaultAsync(m => m.ID == id);
            if (subject == null)
            {
                return NotFound();
            }
            return View(subject);
        }

        // POST: MemberSubjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,SubjectName,Description")] Subject subject)
        {
            if (id != subject.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubjectExists(subject.ID))
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
            return View(subject);
        }

        // GET: MemberSubjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.Subjects
                .SingleOrDefaultAsync(m => m.ID == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // POST: MemberSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subject = await _context.Subjects.SingleOrDefaultAsync(m => m.ID == id);
            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubjectExists(int id)
        {
            return _context.Subjects.Any(e => e.ID == id);
        }
    }
}
