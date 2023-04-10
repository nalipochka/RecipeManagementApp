using System.ComponentModel.DataAnnotations;

namespace RecipeManagementApp.Context.Data
{
    public class Ingredient
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = null!;

        [Required]
        public int Calories { get; set; }

        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; } = null!;
    }
}
