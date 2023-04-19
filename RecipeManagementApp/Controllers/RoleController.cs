using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeManagementApp.Context;

namespace RecipeManagementApp.Controllers
{
    public class RoleController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            return View(await roleManager.Roles.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string title)
        {
            if(!string.IsNullOrEmpty(title))
            {
                IdentityRole role = new IdentityRole(title);
                IdentityResult result = await roleManager.CreateAsync(role);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index", "Role");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Role name can not be empty!");
            }
            return View((Object)title);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if(role == null)
            {
                return NotFound();
            }
            await roleManager.DeleteAsync(role);
            return RedirectToAction("Index", "Role");
        }
    }
}
