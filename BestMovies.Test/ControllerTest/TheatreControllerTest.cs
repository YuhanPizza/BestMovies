using BestMovies.Controllers;
using BestMovies.Interfaces;
using BestMovies.Models;
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
	//The create controller cannot be tested because it is static.
	//the GetUserID
	public class TheatreControllerTest
	{
		private TheatreController _theatreController;
		private readonly ITheatreRepository _theatreRepository;
		private readonly IPhotoService _photoService;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public TheatreControllerTest()
		{
			//Dependency
			//to prepare the values to be used in the test
			_theatreRepository = A.Fake<ITheatreRepository>(); //FakeItEasy 
			_photoService = A.Fake<IPhotoService>();
			_httpContextAccessor = A.Fake<IHttpContextAccessor>();
			//static functions cannot be unit tested

			//SUT - System Under Test
			//we bring in the controller. only usually done in unit tests
			//because controllers are just usually hit by endpoints
			//match the function signature
			_theatreController = new TheatreController(_theatreRepository, _photoService,_httpContextAccessor);
			
		}

		[Fact]
		public void TheatreController_Index_ReturnsSuccess()
		{
			//Arrange what you need to bring in
			var theatre = A.Fake<IEnumerable<Theatre>>();
			//doesnt actually executes the function just fakes it and mocks it 
			//and places in that return fake IEnumerable theatre
			A.CallTo(() => _theatreRepository.GetAll()).Returns(theatre);

			//Act
			var result = _theatreController.Index();

			//Assert - Object Check actions and View Models
			result.Should().BeOfType<Task<IActionResult>>();
		}

		[Fact]
		public void TheatreController_Detail_ReturnsSuccess()
		{
			//Arange
			var id = 1;
			var theatre = A.Fake<Theatre>();
			A.CallTo(()=> _theatreRepository.GetByIdAsync(id)).Returns(theatre);
			//Act
			var result = _theatreController.Detail(id);
			//Asser
			result.Should().BeOfType<Task<IActionResult>>();
		}
	}
}
