using BestMovies.Models;

namespace BestMovies.Interfaces
{
    //interfaces are full of method signature/prototypes
    //we need them because of dependency and polymorphism
    public interface IClubRepository
    {
        //get commands
        Task<IEnumerable<Club>> GetAll();
        Task<Club> GetByIdAsync(int id);
        Task<IEnumerable<Club>> GetClubByCity(string city);

        //cruds
        bool Add(Club club);
        bool Update(Club club);
        bool Delete(Club club);
        bool Save();
    }
}
