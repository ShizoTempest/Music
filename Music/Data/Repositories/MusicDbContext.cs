using Microsoft.EntityFrameworkCore;
using Music.Models;

namespace Music.Data.Repositories;

public class MusicDbContext(DbContextOptions<MusicDbContext> options) : DbContext(options)
{
    public DbSet<Album> Albums { get; set; }
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Song> Songs { get; set; }
    public DbSet<FavoriteArtist> FavoriteArtists { get; set; }
    public DbSet<FavoriteAlbum> FavoriteAlbums { get; set; }
    public DbSet<FavoriteSong> FavoriteSongs { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FavoriteArtist>()
            .HasKey(fa => new { fa.UserId, fa.ArtistId });

        modelBuilder.Entity<FavoriteAlbum>()
            .HasKey(fa => new { fa.UserId, fa.AlbumId });

        modelBuilder.Entity<FavoriteSong>()
            .HasKey(fs => new { fs.UserId, fs.SongId });
    }
}