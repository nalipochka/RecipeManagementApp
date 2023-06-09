using Microsoft.AspNetCore.Mvc.Rendering;
using RecipeManagementApp.Context.Data;
using RecipeManagementApp.Models.RecipeViewModels.DTOs;

namespace RecipeManagementApp.Models.RecipeViewModels
{
    public class AddRecipeViewModel
    {
        public RecipeDTO RecipeDTO { get; set; } = default!;
        public SelectList? CategoriesSl { get; set; }
        public IEnumerable<IFormFile> Images { get; set; } = default!;
    }
}
