using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RecipeManagementApp.Models.UserViewModels
{
    public class EditUserViewModel
    {
        [Required]
        public string Id { get; set; } = default!;
        [Required]
        [DisplayName("Name")]
        public string FirstName { get; set; } = default!;
        [Required]
        [DisplayName("Surname")]
        public string LastName { get; set; } = default!;
        [Required]
        [DisplayName("Date of birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [DisplayName("Login")]
        public string Login { get; set; } = default!;
        [Required]
        [DisplayName("eMail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = default!;
    }
}
