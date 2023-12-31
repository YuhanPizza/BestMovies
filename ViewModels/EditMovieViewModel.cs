﻿using BestMovies.Data.Enum;
using BestMovies.Models;

namespace BestMovies.ViewModels
{
	public class EditMovieViewModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public IFormFile Image { get; set; }
		public string? URL { get; set; }
		public int AddressId { get; set; }
		public Address Address { get; set; }
		public MovieCategory MovieCategory { get; set; }
        public string AppUserId { get; set; }
    }
}
