using AuthDemoProject.Models;
using AuthDemoProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicDBProject.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AuthDemoProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private MusicDbContext context;

        private SongRepository _repo;

        public HomeController(ILogger<HomeController> logger, MusicDbContext dbContext, SongRepository repo)
        {
            _logger = logger;
            context = dbContext;
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

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Detail(int id)
        {
            Song theSong = _repo.FindSongById(id);

            List<SongGenre> songGenres = _repo.FindGenresForSong(id).ToList();

            SongDetailViewModel viewModel = new SongDetailViewModel(theSong, songGenres);
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
