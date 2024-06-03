using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SOA2024.MovieReview.API.Models;
using System.Collections.Generic;

namespace UshtrimeKOL2.Data
{
    public class MovieContext : IdentityDbContext<IdentityUser>
    {
        public MovieContext(DbContextOptions<MovieContext> options)
        : base(options)
        {
        }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
