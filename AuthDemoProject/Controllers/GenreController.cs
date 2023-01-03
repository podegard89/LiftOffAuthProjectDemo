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

        //Repo Method
        //public virtual Genre FindGenreById(int id)
        //{
        //    return _context.Genres.Find(id);
        //}

        //Repo Method
        //public virtual void AddNewGenre(Genre newGenre)
        //{
        //    _context.Genres.Add(newGenre);
        //}

        //Repo Method - changed Genre to SongGenre
        public virtual void AddNewSongGenre(SongGenre newSongGenre)
		{
			_context.SongGenres.Add(newSongGenre);
		}

        public virtual void SaveChanges()
        {
            _context.SaveChanges();
        }

        //Also In Song Controller
		//Ask about concept behind this
        public virtual Song FindSongById(int id)
        {
            return _context.Songs.Include(s => s.Artist).Single(s => s.Id == id);

            //return _context.Songs.Find(id);
        }

        public virtual IEnumerable<Song> FindSongsByArtist(string value)
		{
			return _context.Songs
				.Include(s => s.Artist)
				.Where(s => s.Artist.Name == value)
				.ToList();
			
		}

		//Repo Method - added in
		public virtual IEnumerable<SongGenre> FindGenresForSong(int id)
		{
			return _context.SongGenres
				.Where(sg => sg.SongId == id)
				.Include(sg => sg.Genre).ToList();
		}

		//Repo Method - added in
		public virtual IEnumerable<SongGenre> FindSongGenresByGenre(string value)
		{
			return _context.SongGenres
				.Where(sg => sg.Genre.Name == value)
				.Include(sg => sg.Song)
				.ToList();
		}

        //Repo Method
        public virtual IEnumerable<SongGenre> FindSongsGenresByGenreAndSong(int songId, int genreId)
        {
            return _context.SongGenres
                .Where(sg => sg.SongId == songId)
                .Where(sg => sg.GenreId == genreId)
                .ToList();
        }

		//Repo Method - added in
		public virtual void AddNewGenre(Genre newGenre)
		{
			_context.Genres.Add(newGenre);
		}

  //      public virtual void AddNewSongGenre(SongGenre newSongGenre)
		//{
		//	_context.SongGenres.Add(newSongGenre);
		//}

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
				return Redirect("/Genre/");
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
        public IActionResult Delete()
        {
            ViewBag.genres = _context.Genres.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Delete(int[] genreIds)
        {
            foreach (int genreId in genreIds)
            {
                Genre theGenre = _context.Genres.Find(genreId);
                _context.Genres.Remove(theGenre);
            }
            _context.SaveChanges();

            return Redirect("/Genre");
        }

        public IActionResult About(int id)
		{
			IEnumerable<SongGenre> songGenres = FindSongGenresById(id);
			return View(songGenres);
		}
	}
}

