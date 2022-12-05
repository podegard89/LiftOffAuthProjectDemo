using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthDemoProject.Data;
using AuthDemoProject.Models;
using AuthDemoProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AuthDemoProject.Controllers
{
    public class GenreController : Controller
    {
        private SongRepository _repo { get; set; }

        public GenreController(SongRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            IEnumerable<Genre> genres = _repo.GetAllGenres();
            return View(genres);
        }

        public IActionResult Add()
        {
            AddGenreViewModel addGenreViewModel = new AddGenreViewModel();
            return View(addGenreViewModel);
        }

        public IActionResult ProcessAddGenreForm(AddGenreViewModel addGenreViewModel)
        {
            if (ModelState.IsValid)
            {
                Genre genre = new Genre
                {
                    Type = addGenreViewModel.Type
                };
                _repo.AddNewGenre(genre);
                _repo.SaveChanges();
                return Redirect("/Song");
            }
            return View("Add", addGenreViewModel);
        }

        public IActionResult About(int id)
        {
            IEnumerable<Genre> genres = _repo.GetAllGenres();
            return View();
        }


    }
}

