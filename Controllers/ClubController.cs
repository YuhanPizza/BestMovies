using BestMovies.Data;
using BestMovies.Interfaces;
using BestMovies.Models;
using BestMovies.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BestMovies.Controllers
{//you want your model name then controller. 
    public class ClubController : Controller
    {
        //private readonly ApplicationDbContext _context; //database 
        private readonly IClubRepository _clubRepository; //replaces _context so data is not directly handled
        private readonly IPhotoService _photoService;
        public ClubController(IClubRepository clubRepository, IPhotoService photoService)
        {
            _clubRepository = clubRepository;
            _photoService = photoService;
        }
        public async Task<IActionResult> Index() //the name of the function corellates with your view. 
        {//gets the model then returns the view 
            //defered execution
            IEnumerable<Club> clubs = await _clubRepository.GetAll(); //its bringing a whole table from the database tolist builds up a whole query and brings it to the clubs variable
            return View(clubs); // the clubs is gonna be sent to the view
        }
        public async Task<IActionResult> Detail(int id) {
            //Club club = _context.Clubs.Include(a => a.Address).FirstOrDefault(c => c.Id == id); //include adds the address so it can be displayed in the detail view
            //because in navigational properties they do lazy loading, lazy loading is entity frameworks way of conserving data, where they dont bring in those navigational properties
            //because its a super expensive database call joins are one of those. 
            //a lot of includes is an expensive data base call. 
            Club club = await _clubRepository.GetByIdAsync(id);
            return View(club);
        }
        public IActionResult Create() //can be syncronous 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClubViewModel clubVM)
        {
            if (ModelState.IsValid)
            {
                //return View(club);
                var result = await _photoService.AddPhotoAsync(clubVM.Image);
                var club = new Club
                {
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = result.Url.ToString(),
                    Address = new Address
                    {
                        Street = clubVM.Address.Street,
                        City = clubVM.Address.City,
                        State = clubVM.Address.State,
                    }
                };
                _clubRepository.Add(club);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(clubVM);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var club = await _clubRepository.GetByIdAsync(id);
            if(club == null)
            {
                return View("Error");
            }
            var clubVM = new EditClubViewModel { 
                Title = club.Title, 
                Description = club.Description, 
                AddressId = club.AddressId, 
                Address = club.Address, 
                URL = club.Image, 
                ClubCategory = club.ClubCategory  
            };
            return View(clubVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditClubViewModel clubVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", clubVM);
            }
            var userClub = await _clubRepository.GetByIdAsyncNoTracking(id); //used getbyidasyncnotracking so it wont hit itself because that is being tracked at the moment it is being edited
            if(userClub != null) {
                try
                {
                    await _photoService.DeletePhotoAsync(userClub.Image);
                }
                catch(Exception ex)
                {
					ModelState.AddModelError("", "Failed to delete photo at " + ex);
                    return View(clubVM);
				}
                var photoResult = await _photoService.AddPhotoAsync(clubVM.Image);
                var club = new Club
                {
                    Id = id,
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = photoResult.Url.ToString(),
                    AddressId = clubVM.AddressId,
                    Address = clubVM.Address,
                };

                _clubRepository.Update(club);
                return RedirectToAction("Index");

            }
            else
            {
                return View(clubVM);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            var clubDetails = await _clubRepository.GetByIdAsync(id);
            if (clubDetails == null) {
            return View("Error");
            }
            return View(clubDetails);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var clubDetails = await _clubRepository.GetByIdAsync(id);
            if (clubDetails == null)
            {
                return View("Error");
            }
            _clubRepository.Delete(clubDetails);
            return RedirectToAction("Index");
        }
    }
}
