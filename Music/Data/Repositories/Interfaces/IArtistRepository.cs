using Music.Models;

namespace Music.Data.Repositories.Interfaces
{
    public interface IArtistRepository
    {
        Task<IEnumerable<Artist>> GetAllAsync();
        Task<(IEnumerable<Artist>, PaginationInfo)> GetAllAsync(int pageNumber, int pageSize);
        Task<Artist> GetByIdAsync(int id);
        Task AddAsync(Artist artist);
        Task UpdateAsync(Artist artist);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<List<Album>> GetAlbumsByArtistIdAsync(int artistId);
    }
}
