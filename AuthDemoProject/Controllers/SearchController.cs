using AuthDemoProject.Models;
using AuthDemoProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicDBProject.Data;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace AuthDemoProject.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        private SongRepository _repo;

        public SearchController(SongRepository repo)
        {
            _repo = repo;
        }

        //GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.columns = ListController.ColumnChoices;
            return View();
        }

        public IActionResult Results(string searchType, string searchTerm)
        {
            List<Song> songs;
            List<SongDetailViewModel> displaySongs = new List<SongDetailViewModel>();

            if (string.IsNullOrEmpty(searchTerm))
            {
                songs = _repo.GetAllSongsArtist();

                foreach(var song in songs)
                {
                    List<SongGenre> songGenres = _repo.FindGenresForSong(song.Id).ToList();

                    SongDetailViewModel newDisplaySong = new SongDetailViewModel(song, songGenres);
                    displaySongs.Add(newDisplaySong);
                }
            }
            else
            {
                if (searchType == "artist")
                {
                    songs = _repo.FindSongsByArtist(searchTerm).ToList();

                    foreach (Song song in songs)
                    {
                        List<SongGenre> songGenres = _repo.FindGenresForSong(song.Id).ToList();

                        SongDetailViewModel newDisplaySong = new SongDetailViewModel(song, songGenres);
                        displaySongs.Add(newDisplaySong);
                    }

                }
                else if (searchType == "genre")
                {
                    List<SongGenre> songGenres = _repo.FindSongGenresByGenre(searchTerm).ToList();

                    foreach (var song in songGenres)
                    {
                        Song foundSong = _repo.FindSongBySongGenre(song.SongId);

                        List<SongGenre> displayGenres = _repo.FindGenresForSong(foundSong.Id).ToList();

                        SongDetailViewModel newDisplaySong = new SongDetailViewModel(foundSong, displayGenres);
                        displaySongs.Add(newDisplaySong);
                    }
                }
            }

            ViewBag.columns = ListController.ColumnChoices;
            ViewBag.title = "Songs with " + ListController.ColumnChoices[searchType] + ": " + searchTerm;
            ViewBag.songs = displaySongs;

            return View("Index");
        }

    }
}
