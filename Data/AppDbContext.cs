using Microsoft.EntityFrameworkCore;
using FavoriteMoviesAPI.Models;

namespace FavoriteMoviesAPI.Data
{
    /// <summary>
    /// Contexto do banco de dados para a aplicação.
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Tabelas do banco de dados
        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FavoriteMovie> FavoriteMovies { get; set; }

        /// <summary>
        /// Configuração do modelo de dados usando Fluent API.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da entidade Movie
            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(m => m.Id); // Chave primária
                entity.Property(m => m.Title).IsRequired().HasMaxLength(200); // Título é obrigatório
                entity.Property(m => m.Year).IsRequired(); // Ano é obrigatório
                entity.Property(m => m.Director).HasMaxLength(100); // Diretor tem tamanho máximo de 100 caracteres
                entity.Property(m => m.Poster).HasMaxLength(500); // Poster tem tamanho máximo de 500 caracteres
            });

            // Configuração da entidade User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id); // Chave primária
                entity.Property(u => u.Name).IsRequired().HasMaxLength(100); // Nome é obrigatório
                entity.Property(u => u.Email).IsRequired().HasMaxLength(100); // E-mail é obrigatório
                entity.Property(u => u.Password).IsRequired().HasMaxLength(200); // Senha é obrigatória

                // Relacionamento com FavoriteMovies
                entity.HasMany(u => u.FavoriteMovies)
                      .WithOne(fm => fm.User)
                      .HasForeignKey(fm => fm.UserId);
            });

            // Configuração da entidade FavoriteMovie
            modelBuilder.Entity<FavoriteMovie>(entity =>
            {
                entity.HasKey(fm => fm.Id); // Chave primária
                entity.Property(fm => fm.MovieId).IsRequired(); // MovieId é obrigatório
                entity.Property(fm => fm.UserId).IsRequired(); // UserId é obrigatório

                // Relacionamento com Movie
                entity.HasOne(fm => fm.Movie)
                      .WithMany()
                      .HasForeignKey(fm => fm.MovieId);

                // Relacionamento com User
                entity.HasOne(fm => fm.User)
                      .WithMany(u => u.FavoriteMovies)
                      .HasForeignKey(fm => fm.UserId);
            });
        }
    }
}







