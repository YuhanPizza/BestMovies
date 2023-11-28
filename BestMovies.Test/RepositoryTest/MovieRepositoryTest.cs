using BestMovies.Data.Enum;
using BestMovies.Data;
using BestMovies.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestMovies.Repository;
using FluentAssertions;

namespace BestMovies.Test.RepositoryTest
{
	internal class MovieRepositoryTest
	{
		private async Task<ApplicationDbContext> GetDbContext()
		{
			var options = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) //using InMemoryDatabase for testing purposes 6.0.25
				.Options;
			var databaseContext = new ApplicationDbContext(options);//creating applicationDbContext
			databaseContext.Database.EnsureCreated(); //returns a bool if it is created
			if (await databaseContext.Movies.CountAsync() < 0) //make sures the database is more than 0
			{
				for (int i = 0; i < 10; i++) //so that we dont inject all these manually just for test anyway
				{
					databaseContext.Movies.Add(
						new Movie()
						{
							Title = "Barbie",
							Image = "https://assets.aboutamazon.com/dims4/default/7856190/2147483647/strip/true/crop/1919x1080+1+0/resize/1320x743!/quality/90/?url=https%3A%2F%2Famazon-blogs-brightspot.s3.amazonaws.com%2F7f%2Fe6%2Fb76966994e56a97dbeba44f56009%2Fbarbie-hero.jpg",
							Description = "This is the description of the first movie",
							MovieCategory = MovieCategory.Comedy,
							AddressId = 5,
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
		public async void MovieRepository_Add_ReturnsBool()
		{
			//Arrange
			var movie = new Movie() //create a theatre that we are gonna add to our database
			{
				Title = "Barbie",
				Image = "https://assets.aboutamazon.com/dims4/default/7856190/2147483647/strip/true/crop/1919x1080+1+0/resize/1320x743!/quality/90/?url=https%3A%2F%2Famazon-blogs-brightspot.s3.amazonaws.com%2F7f%2Fe6%2Fb76966994e56a97dbeba44f56009%2Fbarbie-hero.jpg",
				Description = "This is the description of the first movie",
				MovieCategory = MovieCategory.Comedy,
				AddressId = 5,
				Address = new Address()
				{
					Street = "123 Main St",
					City = "Charlotte",
					State = "NC"
				}
			};
			var dbContext = await GetDbContext(); //use the private function we created
			dbContext.Movies.AsNoTracking();// if you get tracking issues 
			var movieRepository = new MovieRepository(dbContext);

			//Act
			var result = movieRepository.Add(movie);

			//ASSERT
			//fluent assertions
			result.Should().BeTrue();//test if its returning true

		}
	}
}
