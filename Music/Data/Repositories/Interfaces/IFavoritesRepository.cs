using Music.Models;

namespace Music.Data.Repositories.Interfaces
{
    public interface IFavoritesRepository
    {
        Task AddFavoriteArtistAsync(int userId, int artistId);
        Task RemoveFavoriteArtistAsync(int userId, int artistId);
        Task<bool> IsArtistFavoriteAsync(int userId, int artistId);
        Task<List<Artist>> GetFavoriteArtistsAsync(int userId);


        Task AddFavoriteAlbumAsync(int userId, int albumId);
        Task RemoveFavoriteAlbumAsync(int userId, int albumId);
        Task<bool> IsAlbumFavoriteAsync(int userId, int albumId);
        Task<List<Album>> GetFavoriteAlbumsAsync(int userId);


        Task AddFavoriteSongAsync(int userId, int songId);
        Task RemoveFavoriteSongAsync(int userId, int songId);
        Task<bool> IsSongFavoriteAsync(int userId, int songId);
        Task<List<Song>> GetFavoriteSongsAsync(int userId);

        Task<bool> ToggleArtistFavoriteAsync(int userId, int artistId);
        Task<bool> ToggleSongFavoriteAsync(int userId, int artistId);
        Task<bool> ToggleAlbumFavoriteAsync(int userId, int artistId);
    }
}
