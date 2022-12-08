using System;
using System.Collections.Generic;
using System.Linq;
using AuthDemoProject.Models;
using AuthDemoProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using MusicDBProject.Data;

namespace AuthDemoProject.Controllers
{
	public class ArtistController : Controller
	{
		private SongRepository _repo;

		public ArtistController(SongRepository repo)
		{
			_repo = repo;
		}

        //GET: /<controller>/
        public IActionResult Index()
        {
            IEnumerable<Artist> artist = _repo.GetAllArtist();
            return View(artist);
        }

        public IActionResult Add()
        {
            AddArtistViewModel addArtistViewModel = new AddArtistViewModel();
            return View(addArtistViewModel);
        }

        public IActionResult ProcessAddEmployerForm(AddArtistViewModel addArtistViewModel)
        {
            if (ModelState.IsValid)
            {
                Artist artist = new Artist
                {
                    Name = addArtistViewModel.Name
                };
                _repo.AddNewArtist(artist);
                _repo.SaveChanges();
                return Redirect("/Employer");
            }
            return View("Add", addArtistViewModel);
        }

        public IActionResult About(int id)
        {
            IEnumerable<Artist> artists = _repo.GetAllArtist();
            return View();
        }

    }
}

