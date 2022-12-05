using AuthDemoProject.Data;
using AuthDemoProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AuthDemoProject.Controllers
{
    public class HomeController : Controller
    {
        private SongRepository _repo;

        public HomeController(SongRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            IEnumerable<Song> songs = _repo.GetAllSongs();
            return View(songs);
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
