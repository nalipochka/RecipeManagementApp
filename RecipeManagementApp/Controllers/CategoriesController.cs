using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecipeManagementApp.Context;
using RecipeManagementApp.Context.Data;
using RecipeManagementApp.Models.CategoriesViewModels;

namespace RecipeManagementApp.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly RecipeManagementContext context;

        public CategoriesController(RecipeManagementContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Category> categories = await context.Categories.ToListAsync();
            return View(categories);
        }

        public IActionResult Create()
        {
            CreateCategoryViewModel vM = new CreateCategoryViewModel()
            {
                ParentCategories = new SelectList(context.Categories, "Id", "Title")
            };
            return View(vM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategoryViewModel vM)
        {
            if (ModelState.IsValid)
            {
                Category category = new Category()
                {
                    Title = vM.Title,
                    ParentCategoryId = vM.ParentCategoryId,
                };
                if(category.ParentCategoryId== 0)
                {
                    category.ParentCategoryId = null;
                }
                context.Add(category);
                await context.SaveChangesAsync();
                return RedirectToAction("Index", "Categories");
            }
            foreach(var error in ModelState.Values.SelectMany(t => t.Errors))
            {
                ModelState.AddModelError(string.Empty, error.ErrorMessage);
            }
            return View(vM);
        }
        
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null || context.Categories == null)
            {
                return NotFound();
            }

            Category? category = await context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            ViewData["ParentCategoryId"] = new SelectList(context.Categories, "Id", "Title", category.ParentCategoryId);
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ParentCategoryId")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (category.ParentCategoryId == 0)
                    {
                        category.ParentCategoryId = null;
                    }
                    context.Update(category);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Categories");
            }
            ViewData["ParentCategoryId"] = new SelectList(context.Categories, "Id", "Title", category.ParentCategoryId);
            return View(category);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null||context.Categories == null)
            {
                return NotFound();
            }

            Category? category = await context.Categories
                .Include(c=>c.ParentCategory)
                .FirstOrDefaultAsync(m=>m.Id==id);
            if(category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmedDelete(int id)
        {
            if(context.Categories == null)
            {
                return Problem("Entity set \"RecipeManagment.Categories\" is null");
            }
            Category? category = await context.Categories.FindAsync(id);
            if(category != null)
            {
                category.ChildCategories = await context.Categories.Where(c => c.ParentCategoryId == id).ToListAsync();
                if(category.ChildCategories != null)
                {
                    foreach (var c in category.ChildCategories)
                    {
                        context.Categories.Remove(c);
                    }
                }
                context.Categories.Remove(category);
            }
            await context.SaveChangesAsync();
            return RedirectToAction("Index", "Categories");
        }

        private bool CategoryExists(int id)
        {
            return context.Categories.Any(e => e.Id == id);
        }
    }
}
