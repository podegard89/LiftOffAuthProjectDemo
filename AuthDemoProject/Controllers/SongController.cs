using System;
using AuthDemoProject.Models;
using Microsoft.AspNetCore.Mvc;
using MusicDBProject.Data;
using System.Collections.Generic;
using AuthDemoProject.ViewModels;
using System.Linq;

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
            return View();
        }

        [HttpGet("/Add")]
        public IActionResult AddSong()
        {
            return View();
        }

        public IActionResult ProcessAddSongForm()
        {
            if (ModelState.IsValid)
            {
                return Redirect("Index");
            }
            return View("Add");
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

        public IActionResult About(int id)
        {
            IEnumerable<Song> songs = _repo.GetAllSongs();
            return View();
        }

        public IActionResult Detail(int id)
        {
            Song theSong = _repo.FindSongById(id);

            List<SongGenre> songGenres = _repo.FindGenresForSong(id).ToList();

            SongDetailViewModel viewModel = new SongDetailViewModel(theSong, songGenres);
            return View(viewModel);
        }
    }
}

