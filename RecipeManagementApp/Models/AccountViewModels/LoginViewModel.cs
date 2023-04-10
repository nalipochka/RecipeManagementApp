using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RecipeManagementApp.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [DisplayName("Login")]
        public string Login { get; set; } = default!;
        [Required]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;
        [DisplayName("Remember me?")]
        public bool IsPersistent { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
