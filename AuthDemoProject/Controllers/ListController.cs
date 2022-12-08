using System;
using System.Collections.Generic;
using System.Linq;
using AuthDemoProject.Models;
using AuthDemoProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
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

		private SongRepository _repo;

		public ListController(SongRepository repo)
		{
			_repo = repo;
		}

		//GET: /<controller>/
		public IActionResult Index()
		{
			ViewBag.columns = ColumnChoices;
			ViewBag.tablechoices = TableChoices;
			ViewBag.songs = _repo.GetAllSongs();
			ViewBag.artist = _repo.GetAllArtist();
			ViewBag.genres = _repo.GetAllGenres();
			return View();
		}

		//list songs by column and value
		public IActionResult Songs(string column, string value)
		{
			List<Song> songs = new List<Song>();
			List<SongDetailViewModel> displaySongs = new List<SongDetailViewModel>();

			if (column.ToLower().Equals("all"))
			{
				songs = _repo.GetAllSongsArtist();

				foreach (var song in songs)
				{
					List<SongGenre> songGenres = _repo.FindGenresForSong(song.Id).ToList();

					SongDetailViewModel newDisplaySong = new SongDetailViewModel(song, songGenres);
					displaySongs.Add(newDisplaySong);
				}

				ViewBag.title = "All Songs";
			}
			else
			{
				if (column == "artist")
				{
					songs = _repo.FindSongsByArtist(value).ToList();

					foreach (Song song in songs)
					{
						List<SongGenre> songGenres = _repo.FindGenresForSong(song.Id).ToList();

						SongDetailViewModel newDisplaySong = new SongDetailViewModel(song, songGenres);
						displaySongs.Add(newDisplaySong);
					}

				}
				else if (column == "genre")
				{
					List<SongGenre> songGenres = _repo.FindSongGenresByGenre(value).ToList();

					foreach (var song in songGenres)
					{
						Song foundSong = _repo.FindSongBySongGenre(song.SongId);

						List<SongGenre> displayGenres = _repo.FindGenresForSong(foundSong.Id).ToList();

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

