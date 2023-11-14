using BestMovies.Data;
using BestMovies.Interfaces;
using BestMovies.Models;
using Microsoft.EntityFrameworkCore;

namespace BestMovies.Repository
{
    //dependency injection and repository pattern
    public class ClubRepository : IClubRepository //when you inherit from an interface make sure you bring in all of the methods
    {
        private readonly ApplicationDbContext _context; //getting the database tables
        public ClubRepository(ApplicationDbContext context) //BRING IN YOUR DATABASE TABLES
        {
            _context = context;
        }
        public bool Add(Club club)
        {
            _context.Add(club); // when you are calling add it is generating all the SQL 
            return Save(); //when save is called that is when it is sending the SQL to the database and creating your entity
        }

        public bool Delete(Club club)
        {
            _context.Remove(club);
            return Save();
        }

        public async Task<IEnumerable<Club>> GetAll() //when you are using async and task you have to use ToListAsync we need to wrap them in task because
            //its returning something but not the actual thing like its a promise like a buzzer for the restaurant they wont give you your table yet but
            //it gives you the buzzer.
        {
            return await _context.Clubs.ToListAsync(); //returns a list
        }

        public async Task<Club> GetByIdAsync(int id)
        {
            return await _context.Clubs.Include(i => i.Address).FirstOrDefaultAsync(i => i.Id == id); //returns only 1 DONT FORGET THE INCLUDE OR ELSE IT WILL DO LAZY LOADING
        }

        public async Task<IEnumerable<Club>> GetClubByCity(string city)
        {
            return await _context.Clubs.Where(c => c.Address.City.Contains(city)).ToListAsync(); //goes into club -> address then search by city.
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Club club)
        {
            _context.Update(club);
            return Save();
        }
    }
}
