using BestMovies.Models;

namespace BestMovies.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Theatre> Theatres { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
