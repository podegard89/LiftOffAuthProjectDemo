using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthDemoProject.Data;
using AuthDemoProject.Models;
using AuthDemoProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AuthDemoProject.Controllers
{
    public class SongController : Controller
    {
        private SongRepository _repo { get; set; }

        public SongController(SongRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
    {
        IEnumerable<Song> songs = _repo.GetAllSongs();
        return View(songs);
    }

    public IActionResult Add()
    {
        AddSongViewModel addSongViewModel = new AddSongViewModel();
        return View(addSongViewModel);
    }

    public IActionResult ProcessAddSongForm(AddSongViewModel addSongViewModel)
    {
        if (ModelState.IsValid)
        {
            Song song = new Song
            {
                Name = addSongViewModel.Name,
                Genre = addSongViewModel.Genre,
                Artist = addSongViewModel.Artist
            };
            _repo.AddNewSong(song);
            _repo.SaveChanges();
            return Redirect("/Song");
        }
        return View("Add", addSongViewModel);
    }

    public IActionResult About(int id)
    {
        IEnumerable<Song> songs = _repo.GetAllSongs();
        return View();
    }
}
}

