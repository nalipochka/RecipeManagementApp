using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RecipeManagementApp.Context.Data
{
    public class Recipe
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
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;

        [Required]
        [ForeignKey("User")]
        public string AuthorId { get; set; } = null!;
        public virtual User Author { get; set; } = null!;

        public int RatingSum { get; set; } // сума всіх оцінок
        public int RatingCount { get; set; } // кількість оцінок

        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; } = null!;
        public virtual ICollection<Image> Images { get; set; } = null!;

        public double GetAverageRating()
        {
            if (RatingCount == 0)
            {
                return 0;
            }
            return (double)RatingSum / RatingCount;
        }

        
        public void AddRating(int rating)
        {
            RatingSum += rating;
            RatingCount++;
        }
    }
}
