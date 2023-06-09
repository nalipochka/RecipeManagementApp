using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecipeManagementApp.Context;
using RecipeManagementApp.Context.Data;
using RecipeManagementApp.Models.RecipeViewModels;
using RecipeManagementApp.Models.RecipeViewModels.DTOs;
using System.Security.Claims;

namespace RecipeManagementApp.Controllers
{
    //[Authorize(Roles = "admin,user")]
    public class RecipeController : Controller
    {
        private readonly RecipeManagementContext context;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly ILogger logger;

        public RecipeController(RecipeManagementContext context, IWebHostEnvironment hostEnvironment, ILoggerFactory logger,
            IMapper mapper, UserManager<User> userManager)
        {
            this.context = context;
            this.hostEnvironment = hostEnvironment;
            this.mapper = mapper;
            this.userManager = userManager;
            this.logger = logger.CreateLogger<RecipeController>();
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AddRecipe()
        {
            AddRecipeViewModel viewModel = new AddRecipeViewModel()
            {
                CategoriesSl = new SelectList(await context.Categories.ToListAsync(), "Id", "Title", 0)
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRecipe(AddRecipeViewModel viewModel)
        {
           if(ModelState.IsValid)
            {
                Recipe recipe = mapper.Map<Recipe>(viewModel.RecipeDTO);
                recipe.AuthorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                recipe.Author = await userManager.FindByIdAsync(recipe.AuthorId);
                List<Image> images = new List<Image>();
                foreach (var image in viewModel.Images)
                {
                    string fileName = $"{Path.GetFileNameWithoutExtension(image.FileName)}" +
                        $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
                    string webRoot = hostEnvironment.WebRootPath + "/images/";
                    string filePath = Path.Combine(webRoot, fileName);
                    using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        await image.CopyToAsync(fileStream);
                    }
                    Image image1 = new Image()
                    {
                        ImageURL = "/images/" + fileName,
                        RecipeId = viewModel.RecipeDTO.Id,
                        Recipe = mapper.Map<Recipe>(viewModel.RecipeDTO)
                    };
                    images.Add(image1);
                }
                await context.Images.AddRangeAsync(images);
                await context.Recipes.AddAsync(recipe);
                await context.SaveChangesAsync();
                return RedirectToAction("Index", "Recipe");
            }
            foreach (var error in ModelState.Values.SelectMany(t => t.Errors))
            {
                logger.LogError(error.ErrorMessage);
            }
            return View(viewModel);
        }
    }
}
