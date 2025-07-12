using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music.Data.Repositories;
using Music.Data.Repositories.Interfaces;

namespace Music.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IAlbumRepository _albumRepository;
    private readonly IArtistRepository _artistRepository;
    private readonly ISongRepository _songRepository;
    private readonly MusicDbContext _context;

    public HomeController(
        MusicDbContext context,
        ILogger<HomeController> logger,
        IAlbumRepository albumRepository,
        IArtistRepository artistRepository,
        ISongRepository songRepository)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _albumRepository = albumRepository ?? throw new ArgumentNullException(nameof(albumRepository));
        _artistRepository = artistRepository ?? throw new ArgumentNullException(nameof(artistRepository));
        _songRepository = songRepository ?? throw new ArgumentNullException(nameof(songRepository));
    }

    public async Task<IActionResult> Index()
    {
        var albums = await _albumRepository.GetAllAsync();
        var artists = await _artistRepository.GetAllAsync();
        var songs = await _songRepository.GetAllAsync();

        ViewBag.AlbumsCount = albums.Count();
        ViewBag.ArtistsCount = artists.Count();
        ViewBag.SongsCount = songs.Count();

        return View();
    }
}
