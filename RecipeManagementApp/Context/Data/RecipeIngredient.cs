using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RecipeManagementApp.Context.Data
{
    public class RecipeIngredient
    {
        [Key, Column(Order = 0)]
        public int RecipeId { get; set; }

        [Key, Column(Order = 1)]
        public int IngredientId { get; set; }

        [Required]
        public string Unit { get; set; } = null!;

        [Required]
        public double Count { get; set; }

        public virtual Recipe Recipe { get; set; } = null!;

        public virtual Ingredient Ingredient { get; set; } = null!;
    }
}
