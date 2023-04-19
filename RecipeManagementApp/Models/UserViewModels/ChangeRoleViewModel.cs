using Microsoft.AspNetCore.Identity;

namespace RecipeManagementApp.Models.UserViewModels
{
    public class ChangeRoleViewModel
    {
        public string Id { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public IList<string> UserRoles { get; set; } = default!;
        public IEnumerable<IdentityRole> AllRoles { get; set; } = default!;
    }
}
