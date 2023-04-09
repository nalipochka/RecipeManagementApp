using System.ComponentModel.DataAnnotations;

namespace RecipeManagementApp.Context.Data
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = null!;

        public int? ParentCategoryId { get; set; }

        public virtual Category ParentCategory { get; set; } = null!;

        public virtual ICollection<Category> ChildCategories { get; set; } = null!;

        public virtual ICollection<Recipe> Recipes { get; set; } = null!;
    }
}
