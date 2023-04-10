using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace RecipeManagementApp.Models.UserViewModel
{
    public class CreateUserViewModel
    {
        [Required]
        [DisplayName("Name")]
        public string FirstName { get; set; } = default!;
        [Required]
        [DisplayName("Surname")]
        public string LastName { get; set; } = default!;
        [Required]
        [DisplayName("Login")]
        public string Login { get; set; } = default!;
        [Required]
        [DisplayName("Date of birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
      
        [Required]
        [DisplayName("eMail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = default!;
        [Required]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;
    }
}
