using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gunluk.Data;

namespace Gunluk.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class KategorilerController : Controller
    {
        private readonly UygulamaDbContext _db;

        public KategorilerController(UygulamaDbContext db)
        {
            _db = db;
        }

        // GET: Admin/Kategoriler
        public async Task<IActionResult> Index()
        {
              return View(await _db.Kategoriler.ToListAsync());
        }

        // GET: Admin/Kategoriler/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _db.Kategoriler == null)
            {
                return NotFound();
            }

            var kategori = await _db.Kategoriler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kategori == null)
            {
                return NotFound();
            }

            return View(kategori);
        }

        // GET: Admin/Kategoriler/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Kategoriler/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ad")] Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                _db.Add(kategori);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kategori);
        }

        // GET: Admin/Kategoriler/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _db.Kategoriler == null)
            {
                return NotFound();
            }

            var kategori = await _db.Kategoriler.FindAsync(id);
            if (kategori == null)
            {
                return NotFound();
            }
            return View(kategori);
        }

        // POST: Admin/Kategoriler/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ad")] Kategori kategori)
        {
            if (id != kategori.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(kategori);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KategoriExists(kategori.Id))
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
            return View(kategori);
        }

        // GET: Admin/Kategoriler/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _db.Kategoriler == null)
            {
                return NotFound();
            }

            var kategori = await _db.Kategoriler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kategori == null)
            {
                return NotFound();
            }

            return View(kategori);
        }

        // POST: Admin/Kategoriler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_db.Kategoriler == null)
            {
                return Problem("Entity set 'UygulamaDbContext.Kategoriler'  is null.");
            }
            var kategori = await _db.Kategoriler.FindAsync(id);
            if (kategori != null)
            {
                _db.Kategoriler.Remove(kategori);
            }
            
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KategoriExists(int id)
        {
          return _db.Kategoriler.Any(e => e.Id == id);
        }
    }
}
