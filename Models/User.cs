using System.Collections.Generic;

namespace FavoriteMoviesAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // Lista de filmes favoritos (agora do tipo FavoriteMovie)
        public List<FavoriteMovie> FavoriteMovies { get; set; } = new();
    }
}


