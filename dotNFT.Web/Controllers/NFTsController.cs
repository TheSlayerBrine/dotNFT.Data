using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using dotNFT.Data;
using dotNFT.Models;

namespace dotNFT.Controllers
{
    public class NFTsController : Controller
    {
        private readonly AppDbContext _context;

        public NFTsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: NFTs
        public async Task<IActionResult> Index()
        {
            var nfts = await _context.NFTs.Include(n => n.Collection)
                .ToListAsync();
            return View(nfts);
        }

        // GET: NFTs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.NFTs == null)
            {
                return NotFound();
            }

            var nFT = await _context.NFTs
                .Include(n => n.Collection)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nFT == null)
            {
                return NotFound();
            }

            return View(nFT);
        }

        // GET: NFTs/Create
        public IActionResult Create()
        {
            ViewData["CollectionId"] = new SelectList(_context.Collections, "Id", "Description");
            return View();
        }

        // POST: NFTs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,ImageURL,MintDate,EndDate,CollectionId,NFTCategory")] NFTViewModel nFT)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nFT);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CollectionId"] = new SelectList(_context.Collections, "Id", "Description", nFT.CollectionId);
            return View(nFT);
        }

        // GET: NFTs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.NFTs == null)
            {
                return NotFound();
            }

            var nFT = await _context.NFTs.FindAsync(id);
            if (nFT == null)
            {
                return NotFound();
            }
            ViewData["CollectionId"] = new SelectList(_context.Collections, "Id", "Description", nFT.CollectionId);
            return View(nFT);
        }

        // POST: NFTs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,ImageURL,MintDate,EndDate,CollectionId,NFTCategory")] NFTViewModel nFT)
        {
            if (id != nFT.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nFT);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NFTExists(nFT.Id))
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
            ViewData["CollectionId"] = new SelectList(_context.Collections, "Id", "Description", nFT.CollectionId);
            return View(nFT);
        }

        // GET: NFTs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.NFTs == null)
            {
                return NotFound();
            }

            var nFT = await _context.NFTs
                .Include(n => n.Collection)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nFT == null)
            {
                return NotFound();
            }

            return View(nFT);
        }

        // POST: NFTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.NFTs == null)
            {
                return Problem("Entity set 'AppDbContext.NFTs'  is null.");
            }
            var nFT = await _context.NFTs.FindAsync(id);
            if (nFT != null)
            {
                _context.NFTs.Remove(nFT);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NFTExists(int id)
        {
          return (_context.NFTs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
