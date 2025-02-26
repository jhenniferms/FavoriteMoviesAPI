using Microsoft.EntityFrameworkCore;
using FavoriteMoviesAPI.Models;

namespace FavoriteMoviesAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
    }
}







