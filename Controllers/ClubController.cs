using BestMovies.Data;
using BestMovies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BestMovies.Controllers
{//you want your model name then controller. 
    public class ClubController : Controller
    {
        private readonly ApplicationDbContext _context; //database 
        public ClubController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index() //the name of the function corellates with your view. 
        {//gets the model then returns the view 
            //defered execution
            List<Club> clubs = _context.Clubs.ToList(); //its bringing a whole table from the database tolist builds up a whole query and brings it to the clubs variable
            return View(clubs); // the clubs is gonna be sent to the view
        }
        public IActionResult Detail(int id) {
            Club club = _context.Clubs.Include(a => a.Address).FirstOrDefault(c => c.Id == id); //include adds the address so it can be displayed in the detail view
            //because in navigational properties they do lazy loading, lazy loading is entity frameworks way of conserving data, where they dont bring in those navigational properties
            //because its a super expensive database call joins are one of those. 
            //a lot of includes is an expensive data base call. 
            return View(club);
        }
    }
}
