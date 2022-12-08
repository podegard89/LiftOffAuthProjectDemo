using System;
using System.Collections.Generic;
using System.Linq;
using AuthDemoProject.Models;
using AuthDemoProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using MusicDBProject.Data;

namespace AuthDemoProject.Controllers
{
	public class GenreController : Controller
	{
		private SongRepository _repo;

		public GenreController(SongRepository repo)
		{
			_repo = repo;
		}

		//GET: /<controller>/
		public IActionResult Index()
		{
			IEnumerable<Genre> genres = _repo.GetAllGenres();
			return View(genres);
		}

		public IActionResult Add()
		{
			Genre genre = new Genre();
			return View(genre);
		}

		[HttpPost]
		public IActionResult Add(Genre genre)
		{
			if (ModelState.IsValid)
			{
				_repo.AddNewGenre(genre);
				_repo.SaveChanges();
				return Redirect("/Genre");
			}

			return View("Add", genre);
		}

		public IActionResult AddSong(int id)
		{
			Song theSong = _repo.FindSongById(id);
			IEnumerable<Genre> possibleGenres = _repo.GetAllGenres();
			AddSongGenreViewModel viewModel = new AddSongGenreViewModel(theSong, possibleGenres.ToList());
			return View(viewModel);
		}

		[HttpPost]
		public IActionResult AddSong(AddSongGenreViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				int songId = viewModel.SongId;
				int genreId = viewModel.GenreId;

				List<SongGenre> existingItems = _repo.FindSongsGenresByGenreAndSong(songId, genreId).ToList();

				if (existingItems.Count == 0)
				{
					SongGenre songGenre = new SongGenre
					{
						SongId = songId,
						GenreId = genreId
					};

					_repo.AddNewSongGenre(songGenre);
					_repo.SaveChanges();
				}

				return Redirect("/Home/Detail/" + songId);
			}

			return View(viewModel);
		}

		public IActionResult About(int id)
		{
			IEnumerable<SongGenre> songGenres = _repo.FindSongGenresById(id);
			return View(songGenres);
		}
	}
}

