using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoPIM4Web.Data;
using ProjetoPIM4Web.Models;
using ProjetoPIM4Web.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoPIM4Web.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<Users> _userManager;
        private readonly AppDbContext _context;

        public UserController(UserManager<Users> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            return View(await _userManager.Users.ToListAsync());
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Users { UserName = model.Email, Email = model.Email, Fullname = model.Fullname, EnterpriseId = model.EnterpriseId, PhoneNumber = model.PhoneNumber };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Fullname,EnterpriseId,PhoneNumber,Email,UserName")] Users user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var userToUpdate = await _userManager.FindByIdAsync(id);
                    userToUpdate.Fullname = user.Fullname;
                    userToUpdate.EnterpriseId = user.EnterpriseId;
                    userToUpdate.PhoneNumber = user.PhoneNumber;
                    userToUpdate.Email = user.Email;
                    userToUpdate.UserName = user.Email; // UserName is usually the email

                    var result = await _userManager.UpdateAsync(userToUpdate);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string id)
        {
            return _userManager.Users.Any(e => e.Id == id);
        }
    }
}

