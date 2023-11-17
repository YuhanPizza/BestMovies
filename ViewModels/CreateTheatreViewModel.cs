using BestMovies.Data.Enum;
using BestMovies.Models;

namespace BestMovies.ViewModels
{
    public class CreateTheatreViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
        public IFormFile Image {  get; set; }
        public TheatreCategory TheatreCategory { get; set; }
        public string AppUserId { get; set; }
    }
}
