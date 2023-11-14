using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BestMovies.Models
{
    public class AppUser : IdentityUser //check definition to see inherited properties
    {
        public int? Pace { get; set; }
        public int? Milage { get; set; }
        [ForeignKey("Address")]
        public int AddressId {  get; set; }
        public Address? Address { get; set; }
        public ICollection<Club> Clubs { get; set; }
        public ICollection<Race> Races { get; set; }
      
    }
}
