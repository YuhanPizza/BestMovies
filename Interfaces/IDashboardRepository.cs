using BestMovies.Models;

namespace BestMovies.Interfaces
{
    //remember to build.services.addscope on program.cs
    public interface IDashboardRepository
    {
        Task<List<Race>> GetAllUserRaces();
        Task<List<Club>> GetAllUserClubs();
        Task <AppUser> GetUserById(string id);
        Task<AppUser> GetByIdNoTracking(string id);
        bool Update (AppUser user);
        bool Save();
    }
}
