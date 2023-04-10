using Microsoft.AspNetCore.Identity;
using RecipeManagementApp.Context.Data;

namespace RecipeManagementApp.Context
{
    public class User:IdentityUser
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public DateTime DateOfBirth { get; set; } = default!;

        public virtual ICollection<Recipe> Recipes { get; set; } = null!;
    }
}
