using Microsoft.EntityFrameworkCore;
using Music.Data.Repositories.Interfaces;
using Music.Filters;
using Music.Models;

namespace Music.Data.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly MusicDbContext _context;

        public ArtistRepository(MusicDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Artist>> GetAllAsync()
        {
            return await _context.Artists.ToListAsync();
        }

        public async Task<Artist> GetByIdAsync(int id)
        {
            return await _context.Artists.FindAsync(id);
        }

        public async Task AddAsync(Artist artist)
        {
            await _context.Artists.AddAsync(artist);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Artist artist)
        {
            _context.Artists.Update(artist);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var artist = await GetByIdAsync(id);
            if (artist != null)
            {
                _context.Artists.Remove(artist);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Artists.AnyAsync(a => a.Id == id);
        }
        public async Task<List<Album>> GetAlbumsByArtistIdAsync(int artistId)
        {
            return await _context.Albums
                .Where(a => a.Id == artistId)
                .ToListAsync();
        }
        public async Task<(IEnumerable<Artist>, PaginationInfo)> GetAllAsync(int pageNumber, int pageSize)
        {
            var totalItems = await _context.Artists.CountAsync();

            var items = await _context.Artists
                .OrderBy(a => a.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var paginationInfo = new PaginationInfo
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems
            };

            return (items, paginationInfo);
        }
        public async Task<List<Artist>> GetFavoriteArtistsAsync(int userId)
        {
            return await _context.FavoriteArtists
                .Where(fa => fa.UserId == userId)
                .Include(fa => fa.Artist)
                .Select(fa => fa.Artist)
                .ToListAsync();
        }
        public async Task<bool> IsFavoriteForUserAsync(int artistId, int userId)
        {
            return await _context.FavoriteArtists
                .AnyAsync(fa => fa.ArtistId == artistId && fa.UserId == userId);
        }
        public async Task<(IEnumerable<Artist>, int)> GetFilteredAsync(ArtistFilter filter)
        {
            var query = _context.Artists
                .Include(a => a.Albums)
                .AsQueryable();

            query = filter.ApplyFilter(query);

            // Сортировка
            if (!string.IsNullOrEmpty(filter.SortBy))
            {
                query = filter.SortBy switch
                {
                    "name" => filter.SortDesc
                        ? query.OrderByDescending(a => a.Name)
                        : query.OrderBy(a => a.Name),
                    "created" => filter.SortDesc
                        ? query.OrderByDescending(a => DateTime.UtcNow)
                        : query.OrderBy(a => DateTime.UtcNow),
                    _ => query
                };
            }

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((filter.Page.Value - 1) * filter.PageSize.Value)
                .Take(filter.PageSize.Value)
                .ToListAsync();

            return (items, totalCount);
        }
    }
}
