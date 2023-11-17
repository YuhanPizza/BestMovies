using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BestMovies.Models
{
    public class AppUser : IdentityUser //check definition to see inherited properties
    {
        public string? ProfileImageUrl { get; set; }
        public string? City {  get; set; }
        public string? State { get; set; }
        [ForeignKey("Address")]
        public int? AddressId {  get; set; }
        public Address? Address { get; set; }
        public ICollection<Theatre> Theatres { get; set; }
        public ICollection<Movie> Movies { get; set; }
      
    }
}
