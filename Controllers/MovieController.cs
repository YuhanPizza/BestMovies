using BestMovies.Data;
using BestMovies.Interfaces;
using BestMovies.Models;
using BestMovies.Repository;
using BestMovies.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BestMovies.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IPhotoService _photoService;
		private readonly IHttpContextAccessor _httpContextAccessor;
        public MovieController(IMovieRepository movieRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            _movieRepository = movieRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Movie> movies = await _movieRepository.GetAll();
            return View(movies);
        }
        public async Task<IActionResult> Detail(int id) {
            //Movie movies = _context.Movies.Include(a=> a.Address).FirstOrDefault(r => r.Id == id);
            Movie movies = await _movieRepository.GetByIdAsync(id);
            return View(movies);
        }
        public IActionResult Create() //can be syncronous 
        {
			var curUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
			var createMovieViewModel = new CreateMovieViewModel()
			{
				AppUserId = curUserId
			};
            return View(createMovieViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMovieViewModel movieVM)
        {
            if (ModelState.IsValid) //checks for validation and returns a model state validation error at create.cshtml asp-validation that I setup 
            {
                //return View(race);
                var result = await _photoService.AddPhotoAsync(movieVM.Image);
                var movie = new Movie
                {
                    Title = movieVM.Title,
                    Description = movieVM.Description,
                    Image = result.Url.ToString(),
                    AppUserId = movieVM.AppUserId,
                    Address = new Address
                    {
                        Street = movieVM.Address.Street,
                        City = movieVM.Address.City,
                        State = movieVM.Address.State,
                    }
                };
                _movieRepository.Add(movie);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");

            }
            return View(movieVM);
        }
		public async Task<IActionResult> Edit(int id)
		{
			var movie = await _movieRepository.GetByIdAsync(id);
			if (movie == null)
			{
				return View("Error");
			}
			var raceVM = new EditMovieViewModel
			{
				Title = movie.Title,
				Description = movie.Description,
				AddressId = movie.AddressId,
				Address = movie.Address,
				URL = movie.Image,
				MovieCategory = movie.MovieCategory,
                AppUserId = movie.AppUserId,

            };
			return View(raceVM);
		}
		[HttpPost]
		public async Task<IActionResult> Edit(int id, EditMovieViewModel movieVM)
		{
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("", "Failed to edit race");
				return View("Edit", movieVM);
			}
			var userMovie = await _movieRepository.GetByIdAsyncNoTracking(id); //used getbyidasyncnotracking so it wont hit itself because that is being tracked at the moment it is being edited
			if (userMovie != null)
			{
				try
				{
					await _photoService.DeletePhotoAsync(userMovie.Image);
				}
				catch (Exception ex)
				{
					ModelState.AddModelError("", "Failed to delete photo at " + ex);
					return View(movieVM);
				}
				var photoResult = await _photoService.AddPhotoAsync(movieVM.Image);
				var movie = new Movie
				{
					Id = id,
					Title = movieVM.Title,
					Description = movieVM.Description,
					Image = photoResult.Url.ToString(),
					AddressId = movieVM.AddressId,
					Address = movieVM.Address,
                    AppUserId = movieVM.AppUserId,
                };

				_movieRepository.Update(movie);
				return RedirectToAction("Index");

			}
			else
			{
				return View(movieVM);
			}
		}
		public async Task<IActionResult> Delete(int id)
		{
			var movieDetails = await _movieRepository.GetByIdAsync(id);
			if (movieDetails == null)
			{
				return View("Error");
			}
			return View(movieDetails);
		}
		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteRace(int id)
		{
			var movieDetails = await _movieRepository.GetByIdAsync(id);
			if (movieDetails == null)
			{
				return View("Error");
			}
			_movieRepository.Delete(movieDetails);
			return RedirectToAction("Index");
		}
	}
}
