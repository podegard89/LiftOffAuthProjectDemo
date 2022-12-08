/*using System;
using AuthDemoProject.Models;
using Microsoft.AspNetCore.Mvc;
using MusicDBProject.Data;
using System.Collections.Generic;
using AuthDemoProject.ViewModels;

namespace MusicDBProject.Controllers
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
            Song song = new Song();
            return View(song);
        }

        [HttpPost]
        public IActionResult Add(Song song)
        {
            if (ModelState.IsValid)
            {
                _repo.AddNewSong(song);
                _repo.SaveChanges();
                return Redirect("/Song");
            }

            return View("Add", song);
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
}*/

