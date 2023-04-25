using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RecipeManagementApp.Models.UserViewModels
{
    public class ChangePasswordUserViewModel
    {
        public string Id { get; set; } = default!;
        //[DisplayName("eMail")]
        //[DataType(DataType.EmailAddress)]
        //public string Email { get; set; }= default!;
        [Required]
        [DisplayName("New password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = default!;
    }
}
