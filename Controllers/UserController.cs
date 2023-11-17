using BestMovies.Interfaces;
using BestMovies.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BestMovies.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet("users")]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsers();
            List<UserViewModel> result = new List<UserViewModel>();
            foreach (var user in users)
            {
                var userViewModel = new UserViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    ProfileImageUrl = user.ProfileImageUrl,
                };
                result.Add(userViewModel);
            }
            return View(result);
        }
        public async Task<IActionResult> Detail(string id) //because its a guid
        {
            var user = await _userRepository.GetUserById(id);
            var userViewModel = new UserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                ProfileImageUrl = user.ProfileImageUrl,
            };
            return View(userViewModel);
        }
    }
}
