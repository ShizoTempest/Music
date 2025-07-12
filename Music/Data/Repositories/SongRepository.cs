using Microsoft.EntityFrameworkCore;
using Music.Data.Repositories.Interfaces;
using Music.Models;

namespace Music.Data.Repositories
{
    public class SongRepository : ISongRepository
    {
        private readonly MusicDbContext _context;

        public SongRepository(MusicDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Song>> GetAllAsync()
        {
            return await _context.Songs.ToListAsync();
        }

        public async Task<Song> GetByIdAsync(int id)
        {
            return await _context.Songs.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddAsync(Song song)
        {
            await _context.Songs.AddAsync(song);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Song song)
        {
            _context.Songs.Update(song);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var song = await GetByIdAsync(id);
            if (song != null)
            {
                _context.Songs.Remove(song);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Songs.AnyAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Song>> GetSongsByAlbumIdAsync(int id)
        {
            return await _context.Songs.Where(s => s.Id == id).ToListAsync();
        }
    }
}
