using System.ComponentModel.DataAnnotations;

namespace RecipeManagementApp.Models.UserViewModel
{
    public class IndexUserViewModel
    {
        [Required]
        public string Id { get; set; } = default!;

        [Required]
        [Display(Name = "Name")]
        public string FirstName { get; set; } = default!;
        [Required]
        [Display(Name = "Surname")]
        public string LastName { get; set; } = default!;
        [Required]
        [Display(Name = "Login")]
        public string Login { get; set; } = default!;

        [Required]
        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "eMail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = default!;
    }
}
