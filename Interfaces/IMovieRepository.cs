using BestMovies.Models;

namespace BestMovies.Interfaces
{
    //interfaces are full of method signature/prototypes
    //we need them because of dependency and polymorphism
    public interface IMovieRepository
    {
        //get commands
        Task<IEnumerable<Movie>> GetAll();
        Task<Movie> GetByIdAsync(int id);
		Task<Movie> GetByIdAsyncNoTracking(int id);
		Task<IEnumerable<Movie>> GetAllMoviesByCity(string city);

        //cruds
        bool Add(Movie race);
        bool Update(Movie race);
        bool Delete(Movie race);
        bool Save();
    }
}
