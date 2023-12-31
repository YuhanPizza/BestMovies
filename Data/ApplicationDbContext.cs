﻿using BestMovies.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BestMovies.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser> //inherits from entityframeworkcore
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        //very important lets us pull stuff out from the database
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Theatre> Theatres { get; set; }
        public DbSet<Address> Addresses { get; set; }

    }
}
