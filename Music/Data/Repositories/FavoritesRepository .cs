using Music.Data.Repositories.Interfaces;
using Music.Models;
using Microsoft.EntityFrameworkCore;

namespace Music.Data.Repositories
{
    public class FavoritesRepository : IFavoritesRepository
    {
        private readonly MusicDbContext _context;

        public FavoritesRepository(MusicDbContext context)
        {
            _context = context;
        }
        public async Task AddFavoriteArtistAsync(int userId, int artistId)
        {
            if (!await _context.FavoriteArtists.AnyAsync(fa => fa.UserId == userId && fa.ArtistId == artistId))
            {
                _context.FavoriteArtists.Add(new FavoriteArtist { UserId = userId, ArtistId = artistId });
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveFavoriteArtistAsync(int userId, int artistId)
        {
            var favorite = await _context.FavoriteArtists
                .FirstOrDefaultAsync(fa => fa.UserId == userId && fa.ArtistId == artistId);

            if (favorite != null)
            {
                _context.FavoriteArtists.Remove(favorite);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsArtistFavoriteAsync(int userId, int artistId)
        {
            return await _context.FavoriteArtists
                .AnyAsync(fa => fa.UserId == userId && fa.ArtistId == artistId);
        }

        public async Task<List<Artist>> GetFavoriteArtistsAsync(int userId)
        {
            return await _context.FavoriteArtists
                .Where(fa => fa.UserId == userId)
                .Include(fa => fa.Artist)
                .Select(fa => fa.Artist)
                .ToListAsync();
        }

        public async Task AddFavoriteAlbumAsync(int userId, int albumId)
        {
            if (!await _context.FavoriteAlbums.AnyAsync(fa => fa.UserId == userId && fa.AlbumId == albumId))
            {
                _context.FavoriteAlbums.Add(new FavoriteAlbum { UserId = userId, AlbumId = albumId });
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveFavoriteAlbumAsync(int userId, int albumId)
        {
            var favorite = await _context.FavoriteAlbums
                .FirstOrDefaultAsync(fa => fa.UserId == userId && fa.AlbumId == albumId);

            if (favorite != null)
            {
                _context.FavoriteAlbums.Remove(favorite);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsAlbumFavoriteAsync(int userId, int albumId)
        {
            return await _context.FavoriteAlbums
                .AnyAsync(fa => fa.UserId == userId && fa.AlbumId == albumId);
        }

        public async Task<List<Album>> GetFavoriteAlbumsAsync(int userId)
        {
            return await _context.FavoriteAlbums
                .Where(fa => fa.UserId == userId)
                .Include(fa => fa.Album)
                .Select(fa => fa.Album)
                .ToListAsync();
        }

        public async Task AddFavoriteSongAsync(int userId, int songId)
        {
            if (!await _context.FavoriteSongs.AnyAsync(fs => fs.UserId == userId && fs.SongId == songId))
            {
                _context.FavoriteSongs.Add(new FavoriteSong { UserId = userId, SongId = songId });
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveFavoriteSongAsync(int userId, int songId)
        {
            var favorite = await _context.FavoriteSongs
                .FirstOrDefaultAsync(fs => fs.UserId == userId && fs.SongId == songId);

            if (favorite != null)
            {
                _context.FavoriteSongs.Remove(favorite);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsSongFavoriteAsync(int userId, int songId)
        {
            return await _context.FavoriteSongs
                .AnyAsync(fs => fs.UserId == userId && fs.SongId == songId);
        }

        public async Task<List<Song>> GetFavoriteSongsAsync(int userId)
        {
            return await _context.FavoriteSongs
                .Where(fs => fs.UserId == userId)
                .Include(fs => fs.Song)
                .Select(fs => fs.Song)
                .ToListAsync();
        }
        public async Task<bool> ToggleArtistFavoriteAsync(int userId, int artistId)
        {
            var isFavorite = await _context.FavoriteArtists
                .AnyAsync(fa => fa.UserId == userId && fa.ArtistId == artistId);

            if (isFavorite)
            {
                var item = await _context.FavoriteArtists
                    .FirstOrDefaultAsync(fa => fa.UserId == userId && fa.ArtistId == artistId);
                _context.FavoriteArtists.Remove(item);
            }
            else
            {
                await _context.FavoriteArtists.AddAsync(new FavoriteArtist
                {
                    UserId = userId,
                    ArtistId = artistId
                });
            }

            await _context.SaveChangesAsync();
            return !isFavorite;
        }
        public async Task<bool> ToggleSongFavoriteAsync(int userId, int songId)
        {
            var isFavorite = await _context.FavoriteSongs
                .AnyAsync(fa => fa.UserId == userId && fa.UserId == userId);

            if (isFavorite)
            {
                var item = await _context.FavoriteSongs
                    .FirstOrDefaultAsync(fa => fa.UserId == userId && fa.UserId == songId);
                _context.FavoriteSongs.Remove(item);
            }
            else
            {
                await _context.FavoriteSongs.AddAsync(new FavoriteSong
                {
                    UserId = userId,
                    SongId = songId
                });
            }

            await _context.SaveChangesAsync();
            return !isFavorite;
        }
        public async Task<bool> ToggleAlbumFavoriteAsync(int userId, int albumId)
        {
            var isFavorite = await _context.FavoriteAlbums
                .AnyAsync(fa => fa.UserId == userId && fa.AlbumId == albumId);

            if (isFavorite)
            {
                var item = await _context.FavoriteAlbums
                    .FirstOrDefaultAsync(fa => fa.UserId == userId && fa.AlbumId == albumId);
                _context.FavoriteAlbums.Remove(item);
            }
            else
            {
                await _context.FavoriteAlbums.AddAsync(new FavoriteAlbum
                {
                    UserId = userId,
                    AlbumId = albumId
                });
            }

            await _context.SaveChangesAsync();
            return !isFavorite;
        }
    }
}
