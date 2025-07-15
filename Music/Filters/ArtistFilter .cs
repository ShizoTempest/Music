using Music.Models;

namespace Music.Filters
{
    public class ArtistFilter : FilterBase<Artist>
    {
        public string Name { get; set; }
        public DateTime? CreatedAfter { get; set; }
        public bool? HasAlbums { get; set; }

        public override IQueryable<Artist> ApplyFilter(IQueryable<Artist> query)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                query = query.Where(a => a.Name.Contains(Name));
            }

            if (CreatedAfter.HasValue)
            {
                query = query.Where(a => DateTime.UtcNow >= CreatedAfter.Value);
            }

            if (HasAlbums.HasValue)
            {
                query = HasAlbums.Value
                    ? query.Where(a => a.Albums.Any())
                    : query.Where(a => !a.Albums.Any());
            }

            return query;
        }
    }
}
