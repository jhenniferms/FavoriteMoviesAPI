namespace FavoriteMoviesAPI.Models
{
    public class FavoriteMovie
    {
        public int Id { get; set; }
        public int MovieId { get; set; } // Chave estrangeira para Movie
        public int UserId { get; set; } // Chave estrangeira para User

        // Relacionamento com Movie
        public Movie Movie { get; set; }

        // Relacionamento com User
        public User User { get; set; }
    }
}
