using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace RecipeManagementApp.Models.UserViewModel
{
    public class DeleteUserViewModel
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
