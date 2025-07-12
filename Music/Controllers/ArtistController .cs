using Microsoft.AspNetCore.Mvc;
using Music.Data.Repositories.Interfaces;
using Music.Models;

namespace Music.Controllers
{
    public class ArtistController : Controller
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IAlbumRepository _albumRepository;

        public ArtistController(
            IArtistRepository artistRepository,
            IAlbumRepository albumRepository)
        {
            _artistRepository = artistRepository;
            _albumRepository = albumRepository;
        }

        public async Task<IActionResult> Index()
        {
            var artists = await _artistRepository.GetAllAsync();
            return View(artists);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Artist artist)
        {
            if (ModelState.IsValid)
            {
                await _artistRepository.AddAsync(artist);
                return RedirectToAction(nameof(Index));
            }
            return View(artist);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var artist = await _artistRepository.GetByIdAsync(id);
            if (artist == null)
            {
                return NotFound();
            }
            return View(artist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Artist artist)
        {
            if (id != artist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _artistRepository.UpdateAsync(artist);
                return RedirectToAction(nameof(Index));
            }
            return View(artist);
        }

        public async Task<IActionResult> Details(int id)
        {
            var artist = await _artistRepository.GetByIdAsync(id);
            if (artist == null)
            {
                return NotFound();
            }

            artist.Albums = await _albumRepository.GetAlbumsByArtistIdAsync(id);

            return View(artist);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var artist = await _artistRepository.GetByIdAsync(id);
            if (artist == null)
            {
                return NotFound();
            }
            return View(artist);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _artistRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var (artists, paginationInfo) = await _artistRepository.GetAllAsync(pageNumber, pageSize);

            ViewBag.PaginationInfo = paginationInfo;
            return View(artists);
        }
    }
}
