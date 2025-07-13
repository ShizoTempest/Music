using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Music.Data.Repositories.Interfaces;
using Music.Models;

namespace Music.Controllers
{
    [Route("api/favorites")]
    [ApiController]
    [Authorize]
    public class FavoritesApiController : ControllerBase
    {
        private readonly IFavoritesRepository _favoritesRepo;
        private readonly UserManager<User> _userManager;

        public FavoritesApiController(
            IFavoritesRepository favoritesRepo,
            UserManager<User> userManager)
        {
            _favoritesRepo = favoritesRepo;
            _userManager = userManager;
        }

        [HttpPost("toggle")]
        public async Task<IActionResult> ToggleFavorite([FromQuery] string type, [FromQuery] int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            bool result;
            switch (type.ToLower())
            {
                case "artist":
                    result = await _favoritesRepo.ToggleArtistFavoriteAsync(user.Id, id);
                    break;
                case "album":
                    result = await _favoritesRepo.ToggleAlbumFavoriteAsync(user.Id, id);
                    break;
                case "song":
                    result = await _favoritesRepo.ToggleSongFavoriteAsync(user.Id, id);
                    break;
                default:
                    return BadRequest("Invalid type");
            }

            return Ok(new { isSuccess = result });
        }
    }
}
