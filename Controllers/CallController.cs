using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoPIM4.Models;
using ProjetoPIM4Web.Data;
using Microsoft.AspNetCore.Identity;
using ProjetoPIM4Web.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoPIM4Web.Controllers
{
    public class CallController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Users> _userManager;

        public CallController(AppDbContext context, UserManager<Users> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Call
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Calls.Include(c => c.ProductService).Include(c => c.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Call/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var call = await _context.Calls
                .Include(c => c.ProductService)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (call == null)
            {
                return NotFound();
            }

            return View(call);
        }

        // GET: Call/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ProductServiceId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await _context.ProductServices.ToListAsync(), "Id", "Name");
            return View();
        }

        // POST: Call/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,ProductServiceId")] Call call)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                {
                    return Unauthorized();
                }
                call.UserId = currentUser.Id;
                call.OpenedDate = DateTime.Now;
                call.Status = "Aberto";
                call.Priority = "Baixa";

                _context.Add(call);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductServiceId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await _context.ProductServices.ToListAsync(), "Id", "Name", call.ProductServiceId);
            return View(call);
        }

        // GET: Call/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var call = await _context.Calls.FindAsync(id);
            if (call == null)
            {
                return NotFound();
            }
            ViewData["ProductServiceId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await _context.ProductServices.ToListAsync(), "Id", "Name", call.ProductServiceId);
            return View(call);
        }

        // POST: Call/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,OpenedDate,ClosedDate,Status,Priority,UserId,ProductServiceId,AiClassification,AiSuggestedSolution,AiTechnicianRecommendation")] Call call)
        {
            if (id != call.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(call);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CallExists(call.Id))
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
            ViewData["ProductServiceId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await _context.ProductServices.ToListAsync(), "Id", "Name", call.ProductServiceId);
            return View(call);
        }

        // GET: Call/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var call = await _context.Calls
                .Include(c => c.ProductService)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (call == null)
            {
                return NotFound();
            }

            return View(call);
        }

        // POST: Call/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var call = await _context.Calls.FindAsync(id);
            _context.Calls.Remove(call);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CallExists(int id)
        {
            return _context.Calls.Any(e => e.Id == id);
        }
    }
}

