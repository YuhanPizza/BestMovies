using BestMovies.Data;
using BestMovies.Interfaces;
using BestMovies.Models;
using BestMovies.ViewModels;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace BestMovies.Controllers
{
	public class DashboardController : Controller
	{
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPhotoService _photoService;

        public DashboardController(IDashboardRepository dashboardRepository,IHttpContextAccessor httpContextAccessor, IPhotoService photoService)
        {
            _httpContextAccessor = httpContextAccessor;
            _photoService = photoService;
            _dashboardRepository = dashboardRepository;
        }
        private void MapUserEdit(AppUser user, EditUserDashboardViewModel editVM, ImageUploadResult photoResult)
        {
            user.Id = editVM.Id;
            user.Pace = editVM.Milage;
            user.Milage = editVM.Milage;
            user.ProfileImageUrl = photoResult.Url.ToString();//cloudinary is going to give you your own url of where it is stored.
            user.City = editVM.City;
            user.State = editVM.State;
        }
        public async Task<IActionResult> Index()
		{
            var userRaces = await _dashboardRepository.GetAllUserRaces();
            var userClubs = await _dashboardRepository.GetAllUserClubs();
            var dashboardViewModel = new DashboardViewModel()
            {
                Races = userRaces,
                Clubs = userClubs,
            };
            return View(dashboardViewModel);
		}
        public async Task<IActionResult> EditUserProfile()
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _dashboardRepository.GetUserById(curUserId); //actually an appuser
            if(user == null)
            {
                return View("Error");
            }
            var editUserViewModel = new EditUserDashboardViewModel()
            {
                Id = curUserId,
                Pace = user.Pace,
                Milage = user.Milage,
                ProfileImageUrl = user.ProfileImageUrl,
                City = user.City,
                State = user.State,
            };
            return View(editUserViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditUserDashboardViewModel editVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Error:Failed to edit Profile");
                return View("EditUserProfile", editVM);
            }
            var user = await _dashboardRepository.GetByIdNoTracking(editVM.Id);//optimistic concurrency - "Tracking ERROR"
                                                                               // use no tracking 
            if (user.ProfileImageUrl == "" || user.ProfileImageUrl == null)
            {
                var photoResult = await _photoService.AddPhotoAsync(editVM.Image);

                MapUserEdit(user,editVM,photoResult);

                _dashboardRepository.Update(user);

                return RedirectToAction("Index");
            }
            else
            {
                try
                {
                    await _photoService.DeletePhotoAsync(user.ProfileImageUrl);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo" + ex);
                    return View(editVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(editVM.Image);

                MapUserEdit(user, editVM, photoResult);

                _dashboardRepository.Update(user);

                return RedirectToAction("Index");
            }
        }
	}
}
