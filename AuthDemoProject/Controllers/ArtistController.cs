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
	[Authorize]
	public class ArtistController : Controller
	{
		private SongRepository _repo { get; set; }

		public ArtistController(SongRepository repo)
		{
			_repo = repo;
		}

		public IActionResult Index()
		{
			IEnumerable<Artist> artists = _repo.GetAllArtist();
			return View(artists);
		}

		public IActionResult Add()
		{
            AddArtistViewModel addArtistViewModel = new AddArtistViewModel();
            return View(addArtistViewModel);
		}

		public IActionResult ProcessAddArtistForm(AddArtistViewModel addArtistViewModel)
		{
			if (ModelState.IsValid)
			{
				Artist artist = new Artist
				{
					Name = addArtistViewModel.Name,
					Song = addArtistViewModel.Song,
					Genre = addArtistViewModel.Genre
				};
				_repo.AddNewArtist(artist);
				_repo.SaveChanges();
				return Redirect("/Artist");
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

