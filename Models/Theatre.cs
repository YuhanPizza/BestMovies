using BestMovies.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BestMovies.Models
{
    public class Theatre
    {
        [Key]//defined key
        public int Id { get; set; } //convention based entity framework automatically detects Id.
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image {  get; set; }
        [ForeignKey("Address")] //a foreign key is important because its what allows other tables have logical relationships 1 to many or many to many foreign key relationship
        //primary key is parent, foreign key is child. Only 1 primary key can exist but many foreign key can exist.
        public int AddressId { get; set; }
        public Address Address { get; set; } //one to many relationship
        public TheatreCategory TheatreCategory { get; set; }
        [ForeignKey("AppUser")]

        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
