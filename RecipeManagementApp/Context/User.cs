using Microsoft.AspNetCore.Identity;
using RecipeManagementApp.Context.Data;

namespace RecipeManagementApp.Context
{
    public class User:IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public virtual ICollection<Recipe> Recipes { get; set; } = null!;
    }
}
