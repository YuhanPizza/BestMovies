using BestMovies.Data;
using BestMovies.Models;
using BestMovies.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BestMovies.Controllers
{
    public class AccountController : Controller
    {
        //bring in user managers
        private readonly UserManager<AppUser> _userManager; //whenever you are doing alot of coding in identity you will see a lot of managers
        private readonly SignInManager<AppUser> _signInManager; //managers provides you with all types of extensions when building authentication controllers
        private readonly ApplicationDbContext _context; //dbcontext
        //constructor to do dependency injections
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        //most of the time get comes with a post
        public IActionResult Login() //get
        {
            var response = new LoginViewModel();//just incase you reloaded it, it will hold the values for you.
            return View(response); //just incase you reloaded it, it will hold the values for you.
        }
        [HttpPost] //post
        public async Task<IActionResult> Login(LoginViewModel loginViewModel) //ViewModels are like a slot where it fits instead of declaring what arguments are gonna be recieved you create a class to store those arguments instead.
        {
            if (!ModelState.IsValid) return View(loginViewModel);

            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);

            if (user != null)
            {
                //user is found, check password
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (passwordCheck)
                {
                    //password correct, sign in
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if(result.Succeeded)
                    {
                        return RedirectToAction("Index", "Race");
                    }
                }
                //password is incorrect
                TempData["Error"] = "Wrong Credentials. Please, try again";
                return View(loginViewModel);
            }
            //user not found
            TempData["Error"] = "Wrong Credentails. Please, try again";
            return View(loginViewModel);
        }
    }
}
