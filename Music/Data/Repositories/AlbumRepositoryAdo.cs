using Microsoft.EntityFrameworkCore;
using Music.Data.Repositories.Interfaces;
using Music.Models;

namespace Music.Data.Repositories;

public class AlbumRepositoryAdo: IAlbumRepository
{
    private readonly MusicDbContext _context;

    public AlbumRepositoryAdo(MusicDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task<List<Album>> GetAllAsync()
    {
        var albums = await _context.Albums.AsNoTracking().ToListAsync();

        return albums;
    }

    public async Task<Album> GetDetailsByIdAsync(int id)
    {
        var album = await _context.Albums
                            .AsNoTracking()
                            .Include(album => album.Songs)
                            .FirstAsync(x => x.Id == id);

        return album;
    }
    public async Task<List<Album>> GetAlbumsByArtistIdAsync(int id)
    {
        return await _context.Albums
            .Where(a => a.Id == id)
            .ToListAsync();
    }
    public async Task<List<Album>> GetFavoriteAlbumsAsync(int userId)
    {
        return await _context.FavoriteAlbums
            .Where(fa => fa.UserId == userId)
            .Include(fa => fa.Album)
            .Select(fa => fa.Album)
            .ToListAsync();
    }
}