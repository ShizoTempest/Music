namespace Music.Models
{
    public class FavoriteArtist
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
    }
}
