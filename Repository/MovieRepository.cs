using BestMovies.Data;
using BestMovies.Interfaces;
using BestMovies.Models;
using Microsoft.EntityFrameworkCore;

namespace BestMovies.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _context;
        public MovieRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Movie movie)
        {
            _context.Add(movie);
            return Save();
        }

        public bool Delete(Movie movie)
        {
            _context.Remove(movie);
            return Save();
        }

        public async Task<IEnumerable<Movie>> GetAll()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesByCity(string city)
        {
            return await _context.Movies.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            return await _context.Movies.Include(i=> i.Address).FirstOrDefaultAsync(i=> i.Id == id); //add include so it doesnt do lazy loading
        }
		public async Task<Movie> GetByIdAsyncNoTracking(int id)
		{
			return await _context.Movies.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id); //add include so it doesnt do lazy loading
		}
		public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true: false;
        }

        public bool Update(Movie race)
        {
            _context.Update(race);
            return Save();
        }
    }
}
