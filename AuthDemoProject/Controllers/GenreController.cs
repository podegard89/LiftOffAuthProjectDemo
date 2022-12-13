using System;
using System.Collections.Generic;
using System.Linq;
using AuthDemoProject.Models;
using AuthDemoProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicDBProject.Data;

namespace AuthDemoProject.Controllers
{
	public class GenreController : Controller
	{
		private readonly MusicDbContext _context;

		public GenreController(MusicDbContext dbContext)
		{
			_context = dbContext;
		}

		public virtual IEnumerable<Genre> GetAllGenres()
		{
			return _context.Genres.ToList();
		}

		public virtual void AddNewGenre(Genre newGenre)
		{
			_context.Genres.Add(newGenre);
		}

        public virtual void SaveChanges()
        {
            _context.SaveChanges();
        }

        public virtual Song FindSongById(int id)
        {
            return _context.Songs.Find(id);
        }

		public virtual IEnumerable<SongGenre> FindSongsGenresByGenreAndSong(int songId, int genreId)
		{
			return _context.SongGenres
				.Where(sg => sg.SongId == songId)
				.Where(sg => sg.GenreId == genreId)
				.ToList();
		}

		public virtual void AddNewSongGenre(SongGenre newSongGenre)
		{
			_context.SongGenres.Add(newSongGenre);
		}

		public virtual IEnumerable<SongGenre> FindSongGenresById(int id)
		{
			return _context.SongGenres
				.Where(sg => sg.GenreId == id)
				.Include(sg => sg.Song)
				.Include(sg => sg.Genre)
				.ToList();
		}


        //GET: /<controller>/
        public IActionResult Index()
		{
			IEnumerable<Genre> genres = GetAllGenres();
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
				AddNewGenre(genre);
				SaveChanges();
				return Redirect("/Genre");
			}

			return View("Add", genre);
		}

		public IActionResult AddSong(int id)
		{
			Song theSong = FindSongById(id);
			IEnumerable<Genre> possibleGenres = GetAllGenres();
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

				List<SongGenre> existingItems = FindSongsGenresByGenreAndSong(songId, genreId).ToList();

				if (existingItems.Count == 0)
				{
					SongGenre songGenre = new SongGenre
					{
						SongId = songId,
						GenreId = genreId
					};

					AddNewSongGenre(songGenre);
					SaveChanges();
				}

				return Redirect("/Home/Detail/" + songId);
			}

			return View(viewModel);
		}

		public IActionResult About(int id)
		{
			IEnumerable<SongGenre> songGenres = FindSongGenresById(id);
			return View(songGenres);
		}
	}
}

