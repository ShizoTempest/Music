namespace Music.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<FavoriteArtist> FavoriteArtists { get; set; }
        public ICollection<FavoriteAlbum> FavoriteAlbums { get; set; }
        public ICollection<FavoriteSong> FavoriteSongs { get; set; }
    }
}
