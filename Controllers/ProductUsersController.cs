using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ProductUsersController : Controller
    {
        private readonly ProductDBContext _context;

        public ProductUsersController(ProductDBContext context)
        {
            _context = context;
        }

        // GET: ProductUsers
        public async Task<IActionResult> Index()
        {
              return _context.ProductUser != null ? 
                          View(await _context.ProductUser.ToListAsync()) :
                          Problem("Entity set 'ProductDBContext.ProductUser'  is null.");
        }

        // GET: ProductUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductUser == null)
            {
                return NotFound();
            }

            var productUser = await _context.ProductUser
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productUser == null)
            {
                return NotFound();
            }

            return View(productUser);
        }

        // GET: ProductUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ProductUser productUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productUser);
        }

        // GET: ProductUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductUser == null)
            {
                return NotFound();
            }

            var productUser = await _context.ProductUser.FindAsync(id);
            if (productUser == null)
            {
                return NotFound();
            }
            return View(productUser);
        }

        // POST: ProductUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ProductUser productUser)
        {
            if (id != productUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductUserExists(productUser.Id))
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
            return View(productUser);
        }

        // GET: ProductUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductUser == null)
            {
                return NotFound();
            }

            var productUser = await _context.ProductUser
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productUser == null)
            {
                return NotFound();
            }

            return View(productUser);
        }

        // POST: ProductUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductUser == null)
            {
                return Problem("Entity set 'ProductDBContext.ProductUser'  is null.");
            }
            var productUser = await _context.ProductUser.FindAsync(id);
            if (productUser != null)
            {
                _context.ProductUser.Remove(productUser);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductUserExists(int id)
        {
          return (_context.ProductUser?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
