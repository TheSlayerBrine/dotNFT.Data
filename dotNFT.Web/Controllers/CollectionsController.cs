using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using dotNFT.Data;
using dotNFT.Models;
using dotNFT.Data.Entities;

namespace dotNFT.Controllers
{
    public class CollectionsController : Controller
    {
        private readonly AppDbContext _context;

        public CollectionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Collections
        public async Task<IActionResult> Index()
        {
            var x = await _context.Collections.ToListAsync();
            var model = x.Select(entity => new dotNFT.Models.CollectionViewModel
            {
                Description = entity.Description,
                Id = entity.Id,
                Name = entity.Name,
                Logo = entity.Logo

            }).ToList();

            return _context.Collections != null ?
                        View(model) :
                        Problem("Entity set 'AppDbContext.Collections'  is null.");
        }

        // GET: Collections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Collections == null)
            {
                return NotFound();
            }

            var collection = await _context.Collections
                .FirstOrDefaultAsync(m => m.Id == id);
            if (collection == null)
            {
                return NotFound();
            }

            var colllectionViewModel = new CollectionViewModel
            {
                Id = id.Value,
                Logo = collection.Logo,
                Name = collection.Name,
                Description = collection.Description,
            };

            return View(colllectionViewModel);
        }

        // GET: Collections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Collections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] CollectionViewModel collection)
        {
            var collectionEntity = new Collection
            {
                Logo = collection.Logo,
                Name = collection.Name,
                Description = collection.Description,
            };
            _context.Add(collectionEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Collections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Collections == null)
            {
                return NotFound();
            }

            var collection = await _context.Collections.FindAsync(id);
            if (collection == null)
            {
                return NotFound();
            }
            var colllectionViewModel = new CollectionViewModel
            {
                Id = id.Value,
                Logo = collection.Logo,
                Name = collection.Name,
                Description = collection.Description,
            };

            return View(colllectionViewModel);
        }

        // POST: Collections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Logo,Name,Description")] CollectionViewModel collection)
        {
            if (id != collection.Id)
            {
                return NotFound();
            }

            try
            {
                var colllectionEntity = new Collection
                {
                    Id = id,
                    Logo = collection.Logo,
                    Name = collection.Name,
                    Description = collection.Description,
                };
                _context.Update(colllectionEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CollectionExists(collection.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // GET: Collections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Collections == null)
            {
                return NotFound();
            }

            var collection = await _context.Collections
                .FirstOrDefaultAsync(m => m.Id == id);
            if (collection == null)
            {
                return NotFound();
            }
            var colllectionViewModel = new CollectionViewModel
            {
                Id = id.Value,
                Logo = collection.Logo,
                Name = collection.Name,
                Description = collection.Description,
            };


            return View(colllectionViewModel);
        }

        // POST: Collections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Collections == null)
            {
                return Problem("Entity set 'AppDbContext.Collections'  is null.");
            }
            var collection = await _context.Collections.FindAsync(id);
            if (collection != null)
            {
                _context.Collections.Remove(collection);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CollectionExists(int id)
        {
            return (_context.Collections?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
