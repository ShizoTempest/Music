﻿using Microsoft.EntityFrameworkCore;
using Music.Data.Repositories.Interfaces;
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
    }
}
