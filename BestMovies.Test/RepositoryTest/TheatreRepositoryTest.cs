using BestMovies.Data;
using BestMovies.Data.Enum;
using BestMovies.Models;
using BestMovies.Repository;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using in memory database 

namespace BestMovies.Test.RepositoryTest
{
	public class TheatreRepositoryTest
	{
		private async Task<ApplicationDbContext> GetDbContext()
		{
			var options = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) //using InMemoryDatabase for testing purposes 6.0.25
				.Options;
			var databaseContext = new ApplicationDbContext(options);//creating applicationDbContext
			databaseContext.Database.EnsureCreated(); //returns a bool if it is created
			if(await databaseContext.Theatres.CountAsync() < 0) //make sures the database is more than 0
			{
				for (int i = 0; i < 10; i++) //so that we dont inject all these manually just for test anyway
				{
					databaseContext.Theatres.Add(
						new Theatre()
						{
							Title = "Cineplex Cinema Empress Walk",
							Image = "https://res.cloudinary.com/dusfrwsg5/image/upload/v1700191021/Cinema_wpvqfd.png",
							Description = "This is the description of the first cinema",
							TheatreCategory = TheatreCategory.IMAX,
							Address = new Address()
							{
								Street = "123 Main St",
								City = "Charlotte",
								State = "NC"
							}
						});
					await databaseContext.SaveChangesAsync();
				}
			}
			return databaseContext;
		}

		[Fact]
		public async void TheatreRepository_Add_ReturnsBool()
		{
			//Arrange
			var theatre = new Theatre() //create a theatre that we are gonna add to our database
			{
				Title = "Cineplex Cinema Empress Walk",
				Image = "https://res.cloudinary.com/dusfrwsg5/image/upload/v1700191021/Cinema_wpvqfd.png",
				Description = "This is the description of the first cinema",
				TheatreCategory = TheatreCategory.IMAX,
				Address = new Address()
				{
					Street = "123 Main St",
					City = "Charlotte",
					State = "NC"
				}
			};
			var dbContext = await GetDbContext(); //use the private function we created
			dbContext.Theatres.AsNoTracking();// if you get tracking issues 
			var theatreRepository = new TheatreRepository(dbContext);

			//Act
			var result = theatreRepository.Add(theatre);

			//ASSERT
			//fluent assertions
			result.Should().BeTrue();//test if its returning true

		}

		[Fact]
		public async void TheatreRepository_GetByIdAsync_ReturnsTheatre() //testing getbyidAsync
		{
			//Arrange
			var id = 1;
			var dbContext = await GetDbContext(); // we bring in our context
			var theatreRepository = new TheatreRepository(dbContext); //the constructor requires that we pass in that context

			//Act
			var result = theatreRepository.GetByIdAsync(id);

			//Assert
			result.Should().NotBeNull(); //should be null
			result.Should().BeOfType<Task<Theatre>>(); //type checking since we dont actullay use real data
		}

		[Fact]
		public async void TheatreRepository_Delete_ReturnsBool()
		{
			//Arrange
			var theatre = new Theatre() //create a theatre that we are gonna add to our database
			{
				Title = "Cineplex Cinema Empress Walk",
				Image = "https://res.cloudinary.com/dusfrwsg5/image/upload/v1700191021/Cinema_wpvqfd.png",
				Description = "This is the description of the first cinema",
				TheatreCategory = TheatreCategory.DriveIn,
				Address = new Address()
				{
					Street = "123 Main St",
					City = "Charlotte",
					State = "NC"
				}
			};
			var dbContext = await GetDbContext(); //use the private function we created
			dbContext.Theatres.AsNoTracking();// if you get tracking issues 
			var theatreRepository = new TheatreRepository(dbContext);
			//if you dont add it first the issue would be that all matching objects will be removed
			//it will cause an issue because the whole repo will be deleted thus no objects are inside of it
			theatreRepository.Add(theatre);//adding the obj first so it matches the delete

			//Act
			var result = theatreRepository.Delete(theatre);//deleting the theatre we just added.

			//ASSERT
			//fluent assertions
			result.Should().BeTrue();//test if its returning true
		}

		[Fact]
		public async void TheatreRepository_GetAll_ReturnsList()
		{
			//Arrange
			var dbContext = await GetDbContext();
			dbContext.Theatres.AsNoTracking();
			var theatreRepository = new TheatreRepository(dbContext);
			//Act
			var result = theatreRepository.GetAll();

			//Assert
			result.Should().BeOfType<Task<IEnumerable<Theatre>>>();
		}
		[Fact]
		public async void TheatreRepository_Update_ReturnsBool()
		{
			//Arrange
			var theatre = new Theatre() //create a theatre that we are gonna add to our database
			{
				Title = "Cineplex Cinema Empress Walk",
				Image = "https://res.cloudinary.com/dusfrwsg5/image/upload/v1700191021/Cinema_wpvqfd.png",
				Description = "This is the description of the first cinema",
				TheatreCategory = TheatreCategory.IMAX,
				Address = new Address()
				{
					Street = "123 Main St",
					City = "Charlotte",
					State = "NC"
				}
			};
			var dbContext = await GetDbContext(); //use the private function we created
			dbContext.Theatres.AsNoTracking();// if you get tracking issues 
			var theatreRepository = new TheatreRepository(dbContext);

			//Act
			var result = theatreRepository.Update(theatre);

			//ASSERT
			//fluent assertions
			result.Should().BeTrue();//test if its returning true

		}

	}
}
