using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoPIM4.Models;
using ProjetoPIM4Web.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoPIM4Web.Controllers
{
    public class ProductServiceController : Controller
    {
        private readonly AppDbContext _context;

        public ProductServiceController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ProductService
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductServices.ToListAsync());
        }

        // GET: ProductService/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productService = await _context.ProductServices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productService == null)
            {
                return NotFound();
            }

            return View(productService);
        }

        // GET: ProductService/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductService/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Category,Description,IsActive")] ProductService productService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productService);
        }

        // GET: ProductService/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productService = await _context.ProductServices.FindAsync(id);
            if (productService == null)
            {
                return NotFound();
            }
            return View(productService);
        }

        // POST: ProductService/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Category,Description,IsActive")] ProductService productService)
        {
            if (id != productService.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductServiceExists(productService.Id))
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
            return View(productService);
        }

        // GET: ProductService/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productService = await _context.ProductServices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productService == null)
            {
                return NotFound();
            }

            return View(productService);
        }

        // POST: ProductService/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productService = await _context.ProductServices.FindAsync(id);
            _context.ProductServices.Remove(productService);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductServiceExists(int id)
        {
            return _context.ProductServices.Any(e => e.Id == id);
        }
    }
}

