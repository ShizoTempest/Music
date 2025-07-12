using Microsoft.AspNetCore.Mvc;
using Music.Data.Repositories;
using Music.Data.Repositories.Interfaces;

namespace Music.Controllers;

public class AlbumController: Controller
{
    private readonly IAlbumRepository _albumRepository;
    private readonly IArtistRepository _artistRepository;
    private readonly MusicDbContext _context;

    public AlbumController(
        MusicDbContext context,
        IAlbumRepository albumRepository,
        IArtistRepository artistRepository)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _albumRepository = albumRepository ?? throw new ArgumentNullException(nameof(albumRepository));
        _artistRepository = artistRepository ?? throw new ArgumentNullException(nameof(artistRepository));
    }
    public async Task<IActionResult> Index()
    {
        var albums = await _albumRepository.GetAllAsync();

        return View(albums);
    }

    public async Task<IActionResult> Details(int id, string name)
    {
        var album = await _albumRepository.GetDetailsByIdAsync(id);

        return View(album);
    }
}