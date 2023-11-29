# BestMovies🎬🍿 [with Unit Tests!]

Welcome to BestMovies, a delightful web application crafted with love using ASP.NET MVC and Entity Framework 6.0 with Identity. 🚀

## Explore Cinematic Wonders Near You 🌍

Discover movie theaters and films in your area based on your IP address. BestMovies makes it easy to find entertainment tailored to your location using the ipinfo.io API. 🎭🎥

## Features 🌟

- **Location Magic:** Seamlessly determines your location via IP address with the help of the ipinfo.io API.
- **Database Mastery:** Manages a localDB with Microsoft SQL Server Manager, providing dedicated tables for theaters, movies, and users.
- **ASP.NET MVC Goodness:** Built on the solid foundations of ASP.NET MVC, featuring well-organized controllers, models, and views.
- **Entity Framework Excellence:** Leverages Entity Framework 6.0 for robust database operations, including migrations and data seeding.
- **User Authentication Awesomeness:** Employs ASP.NET Identity for secure user authentication, with features like login and registration.
- **Cloudinary Charm:** Elevates the visuals using Cloudinary for managing images related to theaters, movies, and user profiles.
- **Repository Pattern Elegance:** Adopts a repository pattern with interfaces for streamlined management of entities.
- **Structure Matters:** Keeps the codebase neat and tidy with organized folders such as Controllers, Models, Repository, Services, Enums, Helpers, Interfaces, and Views.
- **ViewModel Brilliance:** Implements separate view models for different functionalities, ensuring a clean and efficient design.

## Project Structure 🏗️

### Controllers 🎮

- **AccountController:** Your go-to for handling user authentication (Login, Register).
- **DashboardController:** Powers user dashboard-related functionalities.
- **HomeController:** The conductor for the main landing page.
- **MovieController:** Orchestrates movie-related operations (Create, Delete, Detail, Edit, Index).
- **TheatreController:** Directs theater-related operations (Create, Delete, Detail, Edit, Index).
- **UserController:** The maestro for user-related operations.

### Model 🎨

- **Address:** A model for storing address information.
- **AppUsers:** The heart of the application, representing our beloved users.
- **ErrorViewModel:** Ensures graceful handling of errors.
- **Movies:** The star-studded model for storing movie information.
- **Theatre:** The spotlight-stealing model for storing theater information.

### Repository 🗄️

- **DashboardRepository:** Responsible for dashboard-related operations.
- **MovieRepository:** The guardian of movie-related operations.
- **TheatreRepository:** The keeper of theater-related operations.
- **UserRepository:** The custodian of user-related operations.

### Services 🛠️

- **PhotoService:** A trusted companion for handling image-related operations.

### Enums 🌈

- A magical realm containing enums for movie and theater categories.

### Helpers 🤖

- **CloudinarySettings:** The wizard behind the curtain for Cloudinary configuration.
- **IPinfo:** The messenger for making API calls to ipinfo.io.

### Interfaces 🤝

- **IDashboardRepository:** The guide for the dashboard repository.
- **IMovieRepository:** The compass for the movie repository.
- **ITheatreRepository:** The compass for the theater repository.
- **IUserRepository:** The compass for the user repository.

### Views 👀

- Separate folders for each model containing views for different actions.

### Unit Testing 
I use xUnit, FakeItEasy and Fluent Assertions.

- **[xUnit](https://xunit.net/):** A testing framework that keeps things simple and snappy. 🚦

- **[FakeItEasy](https://fakeiteasy.github.io/):** Our backstage pass to mocking – perfect for faking it 'til you make it! 🎭

- **[Fluent Assertions](https://fluentassertions.com/introduction):** Making assertions a breeze with a touch of flair. Fluent and fabulous! 💬

## MVC Controller

**TheatreController:** All Test Passed!

- Index() Passed!
- Detail() Passed!
- Edit() Passed!
- Delete() Passed!

**MovieController:** All Test Passed!

- Index() Passed!
- Detail() Passed!
- Edit() Passed!
- Delete() Passed!

## Repository Test

**TheatreRepository:** All Test Passed!

- Add() Passed!
- GetByIdAsync() Passed!
- Delete() Passed!
- GetAll() Passed!
- Update() Passed!
  
**MovieRepository:** All Test Passed!

- Add() Passed!
- GetByIdAsync() Passed!
- Delete() Passed!
- GetAll() Passed!
- Update() Passed!

## License 📜

This project is licensed under the [MIT License](LICENSE).
