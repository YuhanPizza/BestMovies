﻿using BestMovies.Data;
using BestMovies.Interfaces;
using BestMovies.Models;
using BestMovies.Repository;
using BestMovies.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BestMovies.Controllers
{
    public class RaceController : Controller
    {
        private readonly IRaceRepository _raceRepository;
        private readonly IPhotoService _photoService;
		private readonly IHttpContextAccessor _httpContextAccessor;
        public RaceController(IRaceRepository racerepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            _raceRepository = racerepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Race> races = await _raceRepository.GetAll();
            return View(races);
        }
        public async Task<IActionResult> Detail(int id) {
            //Race race = _context.Races.Include(a=> a.Address).FirstOrDefault(r => r.Id == id);
            Race race = await _raceRepository.GetByIdAsync(id);
            return View(race);
        }
        public IActionResult Create() //can be syncronous 
        {
			var curUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
			var createRaceViewModel = new CreateRaceViewModel()
			{
				AppUserId = curUserId
			};
            return View(createRaceViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRaceViewModel raceVM)
        {
            if (ModelState.IsValid) //checks for validation and returns a model state validation error at create.cshtml asp-validation that I setup 
            {
                //return View(race);
                var result = await _photoService.AddPhotoAsync(raceVM.Image);
                var race = new Race
                {
                    Title = raceVM.Title,
                    Description = raceVM.Description,
                    Image = result.Url.ToString(),
                    AppUserId = raceVM.AppUserId,
                    Address = new Address
                    {
                        Street = raceVM.Address.Street,
                        City = raceVM.Address.City,
                        State = raceVM.Address.State,
                    }
                };
                _raceRepository.Add(race);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");

            }
            return View(raceVM);
        }
		public async Task<IActionResult> Edit(int id)
		{
			var race = await _raceRepository.GetByIdAsync(id);
			if (race == null)
			{
				return View("Error");
			}
			var raceVM = new EditRaceViewModel
			{
				Title = race.Title,
				Description = race.Description,
				AddressId = race.AddressId,
				Address = race.Address,
				URL = race.Image,
				RaceCategory = race.RaceCategory
			};
			return View(raceVM);
		}
		[HttpPost]
		public async Task<IActionResult> Edit(int id, EditRaceViewModel raceVM)
		{
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("", "Failed to edit race");
				return View("Edit", raceVM);
			}
			var userRace = await _raceRepository.GetByIdAsyncNoTracking(id); //used getbyidasyncnotracking so it wont hit itself because that is being tracked at the moment it is being edited
			if (userRace != null)
			{
				try
				{
					await _photoService.DeletePhotoAsync(userRace.Image);
				}
				catch (Exception ex)
				{
					ModelState.AddModelError("", "Failed to delete photo at " + ex);
					return View(raceVM);
				}
				var photoResult = await _photoService.AddPhotoAsync(raceVM.Image);
				var race = new Race
				{
					Id = id,
					Title = raceVM.Title,
					Description = raceVM.Description,
					Image = photoResult.Url.ToString(),
					AddressId = raceVM.AddressId,
					Address = raceVM.Address,
				};

				_raceRepository.Update(race);
				return RedirectToAction("Index");

			}
			else
			{
				return View(raceVM);
			}
		}
		public async Task<IActionResult> Delete(int id)
		{
			var raceDetails = await _raceRepository.GetByIdAsync(id);
			if (raceDetails == null)
			{
				return View("Error");
			}
			return View(raceDetails);
		}
		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteRace(int id)
		{
			var raceDetails = await _raceRepository.GetByIdAsync(id);
			if (raceDetails == null)
			{
				return View("Error");
			}
			_raceRepository.Delete(raceDetails);
			return RedirectToAction("Index");
		}
	}
}
