using System;
using System.Collections.Generic;
using AuthDemoProject.Data;
using AuthDemoProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthDemoProject.Controllers
{
	public class ListController : Controller
	{
		internal static Dictionary<string, string> ColumnChoices = new Dictionary<string, string>()
		{
			{"all", "All" },
			{"artist", "Artist" },
			{"song", "Song" },
			{"genre", "Genre" }
		};

		internal static List<string> TableChoices = new List<string>()
		{
			"artist",
			"song",
			"genre"
		};

		private SongRepository _repo;

        public ListController(SongRepository repo)
		{
			_repo = repo;
		}
		// GET: /<controller>/
		public IActionResult Index()
		{
			ViewBag.columns = ColumnChoices;
			ViewBag.tablechoices = TableChoices;
			ViewBag.artists = _repo.GetAllArtist();
			ViewBag.songs = _repo.GetAllSongs();
			ViewBag.genres = _repo.GetAllGenres();
			return View();
		}

		//list songs by column and value
		public IActionResult Songs(string column, string value)
		{
			List<Song> songs = new List<Song>();
			List<>

		}
	}
}

