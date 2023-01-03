using AuthDemoProject.Models;
using AuthDemoProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MusicDBProject.Controllers;
using MusicDBProject.Data;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AuthDemoProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private MusicDbContext context;


        public HomeController(ILogger<HomeController> logger, MusicDbContext dbContext)
        {
            _logger = logger;
            context = dbContext;

        }

        public IActionResult Index()
        {
            return View();
        }

        
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

        public virtual Song FindSongById(int id)
        {
            return _context.Songs.Include(s => s.Artist).Single(s => s.Id == id);
            //return _context.Songs.Find(id);
        }

        public virtual IEnumerable<SongGenre> FindGenresForSong(int id)
        {
            return _context.SongGenres
                .Where(sg => sg.SongId == id)
                .Include(sg => sg.Genre)
                .ToList();
        }

        public IActionResult Detail(int id)
        {
            Song theSong = FindSongByID(id);
            List<SongGenre> songGenres = FindGenresForSong(id).ToList();
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
