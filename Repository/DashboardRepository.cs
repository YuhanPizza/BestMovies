using BestMovies.Data;
using BestMovies.Interfaces;
using BestMovies.Models;
using Microsoft.EntityFrameworkCore;

namespace BestMovies.Repository
{ //remember to add these on program.cs build.service.addscope
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<Theatre>> GetAllUserTheatres()
        {
            //var curUser = _httpContextAccessor.HttpContext?.User;
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            IQueryable<Theatre> userClubs = _context.Theatres.Where(r => r.AppUser.Id == curUser.ToString());
            return userClubs.ToList();
        }

        public async Task<List<Movie>> GetAllUserMovies()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            IQueryable<Movie> userMovies = _context.Movies.Where(r => r.AppUser.Id == curUser.ToString());
            return userMovies.ToList();
        }
        public async Task<AppUser> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task<AppUser> GetByIdNoTracking(string id)
        {
            return await _context.Users.Where(u=> u.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }
        public bool Update(AppUser user)
        {
            _context.Users.Update(user);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
