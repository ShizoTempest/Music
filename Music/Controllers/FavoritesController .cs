using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Music.Data.Repositories.Interfaces;
using Music.Models;

namespace Music.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly IFavoritesRepository _favoritesRepo;
        private readonly IArtistRepository _artistRepo;
        private readonly IAlbumRepository _albumRepo;
        private readonly ISongRepository _songRepo;
        private readonly UserManager<User> _userManager;

        public FavoritesController(
            IFavoritesRepository favoritesRepo,
            IArtistRepository artistRepo,
            IAlbumRepository albumRepo,
            ISongRepository songRepo,
            UserManager<User> userManager)
        {
            _favoritesRepo = favoritesRepo;
            _artistRepo = artistRepo;
            _albumRepo = albumRepo;
            _songRepo = songRepo;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var model = new FavoritesViewModel
            {
                Artists = await _artistRepo.GetFavoriteArtistsAsync(user.Id),
                Albums = await _albumRepo.GetFavoriteAlbumsAsync(user.Id),
                Songs = await _songRepo.GetFavoriteSongsAsync(user.Id)
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ToggleFavoriteArtist(int artistId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (await _favoritesRepo.IsArtistFavoriteAsync(user.Id, artistId))
            {
                await _favoritesRepo.RemoveFavoriteArtistAsync(user.Id, artistId);
            }
            else
            {
                await _favoritesRepo.AddFavoriteArtistAsync(user.Id, artistId);
            }
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> ToggleFavoriteSong(int artistId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (await _favoritesRepo.IsSongFavoriteAsync(user.Id, artistId))
            {
                await _favoritesRepo.RemoveFavoriteSongAsync(user.Id, artistId);
            }
            else
            {
                await _favoritesRepo.AddFavoriteSongAsync(user.Id, artistId);
            }
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> ToggleFavoriteAlbum(int artistId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (await _favoritesRepo.IsAlbumFavoriteAsync(user.Id, artistId))
            {
                await _favoritesRepo.RemoveFavoriteAlbumAsync(user.Id, artistId);
            }
            else
            {
                await _favoritesRepo.AddFavoriteAlbumAsync(user.Id, artistId);
            }
            return Ok();
        }
    }
}
