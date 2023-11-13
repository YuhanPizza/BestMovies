using BestMovies.Models;

namespace BestMovies.Interfaces
{
    //interfaces are full of method signature/prototypes
    //we need them because of dependency and polymorphism
    public interface IRaceRepository
    {
        //get commands
        Task<IEnumerable<Race>> GetAll();
        Task<Race> GetByIdAsync(int id);
        Task<IEnumerable<Race>> GetAllRacesByCity(string city);

        //cruds
        bool Add(Race race);
        bool Update(Race race);
        bool Delete(Race race);
        bool Save();
    }
}
