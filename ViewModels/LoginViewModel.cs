using System.ComponentModel.DataAnnotations;

namespace BestMovies.ViewModels
{
    //you always wanna put validation in your view models isntead of your domain models
    public class LoginViewModel
    {
        [Display(Name ="Email Address")] //these are called validation annotations
        [Required(ErrorMessage = "Email Address Required")] // if no email address is provided gives error message
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)] //takes away alot of pain in the validation it will also place it in the login view model too
        public string Password { get; set; }
    }
}
