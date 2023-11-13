using BestMovies.Models;
using Microsoft.EntityFrameworkCore;

namespace BestMovies.Data
{
    public class ApplicationDbContext :DbContext //inherits from entityframeworkcore
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        //very important lets us pull stuff out from the database
        public DbSet<Race> Races { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Address> Addresses { get; set; }

        internal void Delete(Club club)
        {
            throw new NotImplementedException();
        }
    }
}
