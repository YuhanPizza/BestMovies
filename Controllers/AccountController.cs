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
        public IActionResult Register() //get
        {
            var response = new RegisterViewModel();//just incase you reloaded it, it will hold the values for you.
            return View(response); //just incase you reloaded it, it will hold the values for you.
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            //model state is when you post, whats gonna happen is the values that you post that you send to this controller/endpoint its gonna pass in
            //logically into this so those values are gonna be placed inside of it. the controller will then do something with the values it recieves
            if (!ModelState.IsValid) //model state is a static class that gives information about the "model state"
            {
                return View(registerViewModel);
            }
            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress); //looks if that email already exists in the database 
            if(user  != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerViewModel);
            }
            var newUser = new AppUser() //creates an appuser from registerViewModel data recieved
            {
                Email = registerViewModel.EmailAddress,
                UserName = registerViewModel.EmailAddress,
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if(newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Race");
        }

    }
}
