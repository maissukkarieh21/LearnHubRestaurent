using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LearnHubRestaurent.Models;

namespace LearnHubRestaurent.Controllers
{
    public class ProductCustomerrsController : Controller
    {
        private readonly ModelContext _context;

        public ProductCustomerrsController(ModelContext context)
        {
            _context = context;
        }

        // GET: ProductCustomerrs
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.ProductCustomerrs.Include(p => p.Customer).Include(p => p.Product);
            return View(await modelContext.ToListAsync());
        }

        // GET: ProductCustomerrs/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.ProductCustomerrs == null)
            {
                return NotFound();
            }

            var productCustomerr = await _context.ProductCustomerrs
                .Include(p => p.Customer)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productCustomerr == null)
            {
                return NotFound();
            }

            return View(productCustomerr);
        }

        // GET: ProductCustomerrs/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customerrs, "Id", "Id");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id");
            return View();
        }

        // POST: ProductCustomerrs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,CustomerId,Quantity,DateFrom,DateTo,Id")] ProductCustomerr productCustomerr)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productCustomerr);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customerrs, "Id", "Id", productCustomerr.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", productCustomerr.ProductId);
            return View(productCustomerr);
        }

        // GET: ProductCustomerrs/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.ProductCustomerrs == null)
            {
                return NotFound();
            }

            var productCustomerr = await _context.ProductCustomerrs.FindAsync(id);
            if (productCustomerr == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customerrs, "Id", "Id", productCustomerr.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", productCustomerr.ProductId);
            return View(productCustomerr);
        }

        // POST: ProductCustomerrs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("ProductId,CustomerId,Quantity,DateFrom,DateTo,Id")] ProductCustomerr productCustomerr)
        {
            if (id != productCustomerr.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productCustomerr);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductCustomerrExists(productCustomerr.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customerrs, "Id", "Id", productCustomerr.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", productCustomerr.ProductId);
            return View(productCustomerr);
        }

        // GET: ProductCustomerrs/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.ProductCustomerrs == null)
            {
                return NotFound();
            }

            var productCustomerr = await _context.ProductCustomerrs
                .Include(p => p.Customer)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productCustomerr == null)
            {
                return NotFound();
            }

            return View(productCustomerr);
        }

        // POST: ProductCustomerrs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.ProductCustomerrs == null)
            {
                return Problem("Entity set 'ModelContext.ProductCustomerrs'  is null.");
            }
            var productCustomerr = await _context.ProductCustomerrs.FindAsync(id);
            if (productCustomerr != null)
            {
                _context.ProductCustomerrs.Remove(productCustomerr);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductCustomerrExists(decimal id)
        {
          return (_context.ProductCustomerrs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
