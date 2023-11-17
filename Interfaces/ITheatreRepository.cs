using BestMovies.Models;

namespace BestMovies.Interfaces
{
    //interfaces are full of method signature/prototypes
    //we need them because of dependency and polymorphism
    public interface ITheatreRepository
    {
        //get commands
        Task<IEnumerable<Theatre>> GetAll();
        Task<Theatre> GetByIdAsync(int id);
		Task<Theatre> GetByIdAsyncNoTracking(int id);
		Task<IEnumerable<Theatre>> GetTheatreByCity(string city);

        //cruds
        bool Add(Theatre club);
        bool Update(Theatre club);
        bool Delete(Theatre club);
        bool Save();
    }
}
