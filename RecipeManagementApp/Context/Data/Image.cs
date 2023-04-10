using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RecipeManagementApp.Context.Data
{
    public class Image
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }

        [Required]
        [StringLength(200)]
        public string ImageURL { get; set; } = null!;

        public virtual Recipe Recipe { get; set; } = null!;
    }
}
