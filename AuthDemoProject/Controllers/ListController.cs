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
	public class ListController : Controller
	{
		internal static Dictionary<string, string> ColumnChoices = new Dictionary<string, string>()
		{
			{"all", "All" },
			{"song", "Song" },
			{"artist", "Artist"},
			{"genre", "Genre" }
		};

		internal static List<string> TableChoices = new List<string>()
		{
			"song",
			"artist",
			"genre"
		};

        private readonly MusicDbContext _context;


        public ListController(MusicDbContext dbcontext)
		{
			_context = dbcontext;
		}

        public virtual IEnumerable<Song> GetAllSongs()
        {
            return _context.Songs
				.Include(s => s.Artist)
				.Include(s => s.Genre)
				.ToList();
        }

        public virtual IEnumerable<Artist> GetAllArtist()
        {
            return _context.Artists
				.Include(a => a.Song)
				.Include(a => a.Genre)
				.ToList();
        }

        public virtual IEnumerable<Genre> GetAllGenres()
        {
            return _context.Genres
				.ToList();
        }

		public List<Song> GetAllSongsArtist()
		{
			return _context.Songs
				.Include(s => s.Artist)
				.ToList();
		}

		public virtual IEnumerable<SongGenre> FindGenresForSong(int id)
		{
			return _context.SongGenres
				.Where(sg => sg.SongId == id)
				.Include(sg => sg.Genre)
				.ToList();
		}

		public virtual IEnumerable<Song> FindSongsByArtist(string value)
		{
			return _context.Songs
				.Include(s => s.Artist)
				.Where(s => s.Artist.Name == value)
				.ToList();
		}

		public virtual IEnumerable<SongGenre> FindSongGenresByGenre(string value)
		{
			return _context.SongGenres
				.Where(s => s.Genre.Name == value)
				.Include(s => s.Song)
				.ToList();
		}

		public virtual Song FindSongBySongGenre(int id)
		{
			return _context.Songs
				.Include(s => s.Artist)
				.Single(s => s.Id == id);
		}

        //GET: /<controller>/
        public IActionResult Index()
		{
			ViewBag.columns = ColumnChoices;
			ViewBag.tablechoices = TableChoices;
			ViewBag.songs = GetAllSongs();
			ViewBag.artist = GetAllArtist();
			ViewBag.genres = GetAllGenres();
			return View();
		}

		//list songs by column and value
		public IActionResult Songs(string column, string value)
		{
			List<Song> songs = new List<Song>();
			List<SongDetailViewModel> displaySongs = new List<SongDetailViewModel>();

			if (column.ToLower().Equals("all"))
			{
				songs = GetAllSongsArtist();

				foreach (var song in songs)
				{
					List<SongGenre> songGenres = FindGenresForSong(song.Id).ToList();

					SongDetailViewModel newDisplaySong = new SongDetailViewModel(song, songGenres);
					displaySongs.Add(newDisplaySong);
				}

				ViewBag.title = "All Songs";
			}
			else
			{
				if (column == "artist")
				{
					songs = FindSongsByArtist(value).ToList();

					foreach (Song song in songs)
					{
						List<SongGenre> songGenres = FindGenresForSong(song.Id).ToList();

						SongDetailViewModel newDisplaySong = new SongDetailViewModel(song, songGenres);
						displaySongs.Add(newDisplaySong);
					}

				}
				else if (column == "genre")
				{
					List<SongGenre> songGenres = FindSongGenresByGenre(value).ToList();

					foreach (var song in songGenres)
					{
						Song foundSong = FindSongBySongGenre(song.SongId);

						List<SongGenre> displayGenres = FindGenresForSong(foundSong.Id).ToList();

						SongDetailViewModel newDisplaySong = new SongDetailViewModel(foundSong, displayGenres);
						displaySongs.Add(newDisplaySong);
					}
				}
				ViewBag.title = "Songs with " + ColumnChoices[column] + ": " + value;
			}
			ViewBag.songs = displaySongs;

			return View();
		}
	}
}

