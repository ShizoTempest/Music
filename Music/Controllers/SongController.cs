using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music.Data.Repositories;
using Music.Data.Repositories.Interfaces;
using Music.Models;

namespace Music.Controllers;

public class SongController : Controller
{
    private readonly ISongRepository _songRepository;
    private readonly IAlbumRepository _albumRepository;

    public SongController(ISongRepository songRepository, IAlbumRepository albumRepository)
    {
        _songRepository = songRepository;
        _albumRepository = albumRepository;
    }

    public async Task<IActionResult> Index()
    {
        var songs = await _songRepository.GetAllAsync();
        return View(songs);
    }

    public async Task<IActionResult> Details(int id)
    {
        var song = await _songRepository.GetByIdAsync(id);
        if (song == null)
        {
            return NotFound();
        }
        return View(song);
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Albums = await _albumRepository.GetAllAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Song song)
    {
        if (ModelState.IsValid)
        {
            await _songRepository.AddAsync(song);
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Albums = await _albumRepository.GetAllAsync();
        return View(song);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var song = await _songRepository.GetByIdAsync(id);
        if (song == null)
        {
            return NotFound();
        }
        ViewBag.Albums = await _albumRepository.GetAllAsync();
        return View(song);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Song song)
    {
        if (id != song.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _songRepository.UpdateAsync(song);
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Albums = await _albumRepository.GetAllAsync();
        return View(song);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var song = await _songRepository.GetByIdAsync(id);
        if (song == null)
        {
            return NotFound();
        }
        return View(song);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _songRepository.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}