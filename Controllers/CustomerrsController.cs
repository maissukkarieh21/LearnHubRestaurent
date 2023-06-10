using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LearnHubRestaurent.Models;
using Microsoft.AspNetCore.Hosting;

namespace LearnHubRestaurent.Controllers
{
    public class CustomerrsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;
        public CustomerrsController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
        }

        // GET: Customerrs
        public async Task<IActionResult> Index()
        {
              return _context.Customerrs != null ? 
                          View(await _context.Customerrs.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Customerrs'  is null.");
        }


        // GET: Customerrs/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Customerrs == null)
            {
                return NotFound();
            }

            var customerr = await _context.Customerrs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerr == null)
            {
                return NotFound();
            }

            return View(customerr);
        }

        // GET: Customerrs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customerrs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Fname,Lname,ImageFile,Id")] Customerr customerr)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnviroment.WebRootPath;
                string fileName = Guid.NewGuid().ToString() + "_" + customerr.ImageFile.FileName;
                string path = Path.Combine(wwwRootPath + "/Customers_image/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))

                {
                    await customerr.ImageFile.CopyToAsync(fileStream);
                }
                customerr.ImagePath = fileName;
                _context.Add(customerr);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customerr);
        }

        // GET: Customerrs/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Customerrs == null)
            {
                return NotFound();
            }

            var customerr = await _context.Customerrs.FindAsync(id);
            if (customerr == null)
            {
                return NotFound();
            }
            return View(customerr);
        }

        // POST: Customerrs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: Customerrs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Fname,Lname,ImageFile,Id,ImagePath")] Customerr customerr)
        {
            if (id != customerr.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingCustomerr = await _context.Customerrs.FindAsync(id);
                    if (existingCustomerr == null)
                    {
                        return NotFound();
                    }

                    if (customerr.ImageFile != null)
                    {
                        // Delete the previous image if it exists
                        if (!string.IsNullOrEmpty(existingCustomerr.ImagePath))
                        {
                            var previousImagePath = Path.Combine(_webHostEnviroment.WebRootPath, "Customers_image", existingCustomerr.ImagePath);
                            if (System.IO.File.Exists(previousImagePath))
                            {
                                System.IO.File.Delete(previousImagePath);
                            }
                        }

                        // Save the new image
                        string wwwRootPath = _webHostEnviroment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + customerr.ImageFile.FileName;
                        string path = Path.Combine(wwwRootPath, "Customers_image", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await customerr.ImageFile.CopyToAsync(fileStream);
                        }

                        existingCustomerr.ImagePath = fileName;
                    }

                    existingCustomerr.Fname = customerr.Fname;
                    existingCustomerr.Lname = customerr.Lname;

                    _context.Update(existingCustomerr);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerrExists(customerr.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(customerr);
        }
        // GET: Customerrs/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Customerrs == null)
            {
                return NotFound();
            }

            var customerr = await _context.Customerrs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerr == null)
            {
                return NotFound();
            }

            return View(customerr);
        }

       
        // POST: Customerrs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Customerrs == null)
            {
                return Problem("Entity set 'ModelContext.Customerrs' is null.");
            }

            var customerr = await _context.Customerrs.FindAsync(id);
            if (customerr == null)
            {
                return NotFound();
            }

            try
            {
                // Delete the image file if it exists
                if (!string.IsNullOrEmpty(customerr.ImagePath))
                {
                    var imagePath = Path.Combine(_webHostEnviroment.WebRootPath, "Customers_image", customerr.ImagePath);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                _context.Customerrs.Remove(customerr);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                // Handle any errors that occur during deletion
                return Problem("An error occurred while deleting the customer.");
            }
        }


        private bool CustomerrExists(decimal id)
        {
          return (_context.Customerrs?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        public async Task<IActionResult> Profile(decimal? id)
        {
            if (id == null || _context.Customerrs == null)
            {
                return NotFound();
            }

            var customerr = await _context.Customerrs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerr == null)
            {
                return NotFound();
            }

            return View(customerr);

        }

    }
}
