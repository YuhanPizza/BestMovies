using BestMovies.Models;

namespace BestMovies.Interfaces
{
    //remember to build.services.addscope on program.cs
    public interface IDashboardRepository
    {
        Task<List<Movie>> GetAllUserMovies();
        Task<List<Theatre>> GetAllUserTheatres();
        Task <AppUser> GetUserById(string id);
        Task<AppUser> GetByIdNoTracking(string id);
        bool Update (AppUser user);
        bool Save();
    }
}
