using BestMovies.Data;
using BestMovies.Interfaces;
using BestMovies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BestMovies.Controllers
{
    public class RaceController : Controller
    {
        private readonly IRaceRepository _raceRepository;
        public RaceController(IRaceRepository racerepository)
        {
            _raceRepository = racerepository;
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
    }
}
