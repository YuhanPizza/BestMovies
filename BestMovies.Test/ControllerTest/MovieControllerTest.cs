using BestMovies.Controllers;
using BestMovies.Interfaces;
using BestMovies.Models;
using BestMovies.Repository;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestMovies.Test.ControllerTest
{
	public class MovieControllerTest
	{
		private MovieController _movieController;
		private readonly IMovieRepository _movieRepository;
		private readonly IPhotoService _photoService;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public MovieControllerTest() //constructor
		{
			//Dependencies
			_movieRepository = A.Fake<IMovieRepository>(); //Fake IT EASY
			_photoService = A.Fake<IPhotoService>();
			_httpContextAccessor = A.Fake<HttpContextAccessor>();

			//SUT - System Under Test
			_movieController = new MovieController(_movieRepository, _photoService, _httpContextAccessor);
		}
		[Fact]

		public void MovieController_Index_ReturnsSuccess()
		{
			//Arrange 
			var movie = A.Fake<IEnumerable<Movie>>();
			A.CallTo(() => _movieRepository.GetAll()).Returns(movie);

			//Act
			var result = _movieController.Index();

			//Assert
			result.Should().BeOfType<Task<IActionResult>>();
		}

		[Fact]
		public void MovieController_Detail_ReturnsSuccess()
		{
			//Arange
			var id = 1;
			var movie = A.Fake<Movie>();
			A.CallTo(() => _movieRepository.GetByIdAsync(id)).Returns(movie);
			//Act
			var result = _movieController.Detail(id);
			//Asser
			result.Should().BeOfType<Task<IActionResult>>();
		}

	}
}