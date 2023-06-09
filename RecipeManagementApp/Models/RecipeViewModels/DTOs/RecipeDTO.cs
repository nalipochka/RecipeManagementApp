using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RecipeManagementApp.Models.RecipeViewModels.DTOs
{
    public class RecipeDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = null!;

        [Required]
        [StringLength(1000)]
        public string Instruction { get; set; } = null!;

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
    }
}
