using System.ComponentModel.DataAnnotations;

namespace BestMovies.Models
{   //models POCO Plain Old CLR objects CLR = Common Language Runtime
    //when building models you are basically doing abstraction: abstracting away the complexity of the app.
    //object model thinking abstraction separating different database tables for Clubs, Races; 
    public class Address
    {
        [Key]
        public int Id { get; set; } //acess modifier public constraint integer data type.
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
