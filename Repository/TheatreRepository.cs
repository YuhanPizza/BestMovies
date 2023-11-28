using BestMovies.Data;
using BestMovies.Interfaces;
using BestMovies.Models;
using Microsoft.EntityFrameworkCore;

namespace BestMovies.Repository
{
    //dependency injection and repository pattern
    public class TheatreRepository : ITheatreRepository //when you inherit from an interface make sure you bring in all of the methods
    {
        private readonly ApplicationDbContext _context; //getting the database tables
        public TheatreRepository(ApplicationDbContext context) //BRING IN YOUR DATABASE TABLES
        {
            _context = context;
        }
        public bool Add(Theatre theatre)
        {
            _context.Add(theatre); // when you are calling add it is generating all the SQL 
            return Save(); //when save is called that is when it is sending the SQL to the database and creating your entity
        }

        public bool Delete(Theatre theatre)
        {
            _context.Remove(theatre);
            return Save();
        }

        public async Task<IEnumerable<Theatre>> GetAll() //when you are using async and task you have to use ToListAsync we need to wrap them in task because
            //its returning something but not the actual thing like its a promise like a buzzer for the restaurant they wont give you your table yet but
            //it gives you the buzzer.
        {
            return await _context.Theatres.ToListAsync(); //returns a list
        }

        public async Task<Theatre> GetByIdAsync(int id)
        {
            return await _context.Theatres.Include(i => i.Address).FirstOrDefaultAsync(i => i.Id == id); //returns only 1 DONT FORGET THE INCLUDE OR ELSE IT WILL DO LAZY LOADING
        }

		public async Task<Theatre> GetByIdAsyncNoTracking(int id)
		{
			return await _context.Theatres.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id); //returns only 1 DONT FORGET THE INCLUDE OR ELSE IT WILL DO LAZY LOADING
		}
		public async Task<IEnumerable<Theatre>> GetTheatreByCity(string city)
        {
            return await _context.Theatres.Where(c => c.Address.City.Contains(city)).ToListAsync(); //goes into club -> address then search by city.
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Theatre theatre)
        {
            _context.Update(theatre);
            return Save();
        }
    }
}
