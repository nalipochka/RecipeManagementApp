using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RecipeManagementApp.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [DisplayName("Name")]
        public string FirstName { get; set; } = default!;
        [Required]
        [DisplayName("Surname")]
        public string LastName { get; set; } = default!;
        [Required]
        [DisplayName("Date of birth")]
        public DateTime DateOfBirth { get; set; } = default!;
        [Required]
        [DisplayName("Login")]
        public string Login { get; set; } = default!;
        [Required]
        [DisplayName("eMail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }= default!;
        [Required]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;
        [Required]
        [DisplayName("Confirm password")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Passwords must match!")]
        public string ConfirmPassword { get; set;} = default!;

    }
}
