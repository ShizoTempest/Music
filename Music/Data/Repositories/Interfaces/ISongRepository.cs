using Music.Models;

namespace Music.Data.Repositories.Interfaces
{
    public interface ISongRepository
    {
        Task<IEnumerable<Song>> GetAllAsync();
        Task<Song> GetByIdAsync(int id);
        Task AddAsync(Song song);
        Task UpdateAsync(Song song);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<Song>> GetSongsByAlbumIdAsync(int albumId);
    }
}
