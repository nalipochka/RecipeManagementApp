
using Microsoft.AspNetCore.Mvc.Rendering;
using RecipeManagementApp.Context.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeManagementApp.Models.CategoriesViewModels
{
    public class CreateCategoryViewModel
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = default!;
        [Required]
        [DisplayName("Parent category")]
        public int ParentCategoryId { get; set; }
        public SelectList? ParentCategories { get; set; } = default!;
    }
}
