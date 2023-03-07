using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Data.ViewModels
{
    public class LoginVM
    {
        [Display(Name = "Email address")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email required")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
