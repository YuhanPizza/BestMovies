using BestMovies.Data;
using BestMovies.Interfaces;
using BestMovies.Models;
using BestMovies.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BestMovies.Controllers
{//you want your model name then controller. 
    public class TheatreController : Controller
    {
        //private readonly ApplicationDbContext _context; //database 
        private readonly ITheatreRepository _theatreRepository; //replaces _context so data is not directly handled
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TheatreController(ITheatreRepository theatreRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            _theatreRepository = theatreRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index() //the name of the function corellates with your view. 
        {//gets the model then returns the view 
            //defered execution
            IEnumerable<Theatre> theatre = await _theatreRepository.GetAll(); //its bringing a whole table from the database tolist builds up a whole query and brings it to the clubs variable
            return View(theatre); // the clubs is gonna be sent to the view
        }
        public async Task<IActionResult> Detail(int id) {
            //Club club = _context.Clubs.Include(a => a.Address).FirstOrDefault(c => c.Id == id); //include adds the address so it can be displayed in the detail view
            //because in navigational properties they do lazy loading, lazy loading is entity frameworks way of conserving data, where they dont bring in those navigational properties
            //because its a super expensive database call joins are one of those. 
            //a lot of includes is an expensive data base call. 
            Theatre theatre = await _theatreRepository.GetByIdAsync(id);
            return View(theatre);
        }
        public IActionResult Create() //can be syncronous 
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var createTheatreViewModel = new CreateTheatreViewModel
            {
                AppUserId = curUserId
            };
            return View(createTheatreViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTheatreViewModel theatreVM)
        {
            if (ModelState.IsValid)
            {
                //return View(club);
                var result = await _photoService.AddPhotoAsync(theatreVM.Image);
                var theatre = new Theatre
                {
                    Title = theatreVM.Title,
                    Description = theatreVM.Description,
                    Image = result.Url.ToString(),
                    AppUserId = theatreVM.AppUserId,
                    Address = new Address
                    {
                        Street = theatreVM.Address.Street,
                        City = theatreVM.Address.City,
                        State = theatreVM.Address.State,
                    }
                };
                _theatreRepository.Add(theatre);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(theatreVM);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var theatre = await _theatreRepository.GetByIdAsync(id);
            if(theatre == null)
            {
                return View("Error");
            }
            var theatreVM = new EditTheatreViewModel { 
                Title = theatre.Title, 
                Description = theatre.Description, 
                AddressId = theatre.AddressId, 
                Address = theatre.Address, 
                URL = theatre.Image, 
                TheatreCategory = theatre.TheatreCategory,
                AppUserId = theatre.AppUserId,
            };
            return View(theatreVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditTheatreViewModel theatreVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit Theatre");
                return View("Edit", theatreVM);
            }
            var userTheatre = await _theatreRepository.GetByIdAsyncNoTracking(id); //used getbyidasyncnotracking so it wont hit itself because that is being tracked at the moment it is being edited
            if(userTheatre != null) {
                try
                {
                    await _photoService.DeletePhotoAsync(userTheatre.Image);
                }
                catch(Exception ex)
                {
					ModelState.AddModelError("", "Failed to delete photo at " + ex);
                    return View(theatreVM);
				}
                var photoResult = await _photoService.AddPhotoAsync(theatreVM.Image);
                var theatre = new Theatre
                {
                    Id = id,
                    Title = theatreVM.Title,
                    Description = theatreVM.Description,
                    Image = photoResult.Url.ToString(),
                    AddressId = theatreVM.AddressId,
                    Address = theatreVM.Address,
                    AppUserId = theatreVM.AppUserId,
                };

                _theatreRepository.Update(theatre);
                return RedirectToAction("Index");

            }
            else
            {
                return View(theatreVM);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            var theatreDetails = await _theatreRepository.GetByIdAsync(id);
            if (theatreDetails == null) {
            return View("Error");
            }
            return View(theatreDetails);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var theatreDetails = await _theatreRepository.GetByIdAsync(id);
            if (theatreDetails == null)
            {
                return View("Error");
            }
            _theatreRepository.Delete(theatreDetails);
            return RedirectToAction("Index");
        }
    }
}
