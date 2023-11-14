using System.ComponentModel.DataAnnotations;

namespace BestMovies.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "EmailAddress is required")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)] //checks for data type
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage ="Confirm Password is Required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Password do not match")]
        public string ConfirmPassword { get; set; }

    }
}
