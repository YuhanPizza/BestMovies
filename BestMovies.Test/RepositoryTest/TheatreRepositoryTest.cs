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
				.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
				.Options;
			var databaseContext = new ApplicationDbContext(options);
			databaseContext.Database.EnsureCreated();
			if(await databaseContext.Theatres.CountAsync() < 0)
			{
				for (int i = 0; i < 10; i++)
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
			var theatre = new Theatre()
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
			var dbContext = await GetDbContext();
			var theatreRepository = new TheatreRepository(dbContext);

			//Act
			var result = theatreRepository.Add(theatre);

			//ASSERT
			result.Should().BeTrue();//test if its returning true

		}
	}
}
