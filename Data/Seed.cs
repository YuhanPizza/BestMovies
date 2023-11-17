using BestMovies.Data.Enum;
using BestMovies.Data;
using BestMovies.Models;
using Microsoft.AspNetCore.Identity;


namespace BestMovies.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.Theatres.Any())
                {
                    context.Theatres.AddRange(new List<Theatre>()
                    {
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
                         },
                        new Theatre()
                        {
                            Title = "Imagine Cinemas Promenade",
                            Image = "https://res.cloudinary.com/dusfrwsg5/image/upload/v1700191021/Cinema_wpvqfd.png",
                            Description = "This is the description of the second cinema",
                            TheatreCategory = TheatreCategory.Premium,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                State = "NC"
                            }
                        },
                        new Theatre()
                        {
                            Title = "Cineplex Cinemas Fairview Mall",
                            Image = "https://res.cloudinary.com/dusfrwsg5/image/upload/v1700191021/Cinema_wpvqfd.png",
                            Description = "This is the description of the second cinema",
                            TheatreCategory = TheatreCategory.DriveIn,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                State = "NC"
                            }
                        },
                        new Theatre()
                        {
                            Title = "Silver City Richmond Hill Cinemas",
                            Image = "https://res.cloudinary.com/dusfrwsg5/image/upload/v1700191021/Cinema_wpvqfd.png",
                            Description = "This is the description of the first cinema",
                            TheatreCategory = TheatreCategory.Multiplex,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Michigan",
                                State = "NC"
                            }
                        }
                    });
                    context.SaveChanges();
                }
                //Races
                if (!context.Movies.Any())
                {
                    context.Movies.AddRange(new List<Movie>()
                    {
                        new Movie()
                        {
                            Title = "Mask of Zorro",
                            Image = "https://m.media-amazon.com/images/M/MV5BMzg4ZjQ4OGUtZjkxMi00Y2I2LWEzNTAtODI2ZjkxMGVjNTQwXkEyXkFqcGdeQXVyNjgxNTAwNjQ@._V1_.jpg",
                            Description = "This is the description of the first movie",
                            MovieCategory = MovieCategory.Western,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Charlotte",
                                State = "NC"
                            }
                        },
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
                        }
                    });
                    context.SaveChanges();
                }
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "teddysmithdeveloper@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "teddysmithdev",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "123 Main St",
                            City = "Charlotte",
                            State = "NC"
                        }
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@etickets.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "123 Main St",
                            City = "Charlotte",
                            State = "NC"
                        }
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}