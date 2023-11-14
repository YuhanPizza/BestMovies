using BestMovies.Data;
using BestMovies.Interfaces;
using BestMovies.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BestMovies.Controllers
{
	public class DashboardController : Controller
	{
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardController(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
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
	}
}
