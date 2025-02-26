namespace FavoriteMoviesAPI.Models
{
    public class FavoriteMovie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string Director { get; set; }
        public string Poster { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
