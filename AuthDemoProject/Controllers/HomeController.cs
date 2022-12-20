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

        public IActionResult Detail(int id)
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
