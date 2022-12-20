using AuthDemoProject.Models;
using AuthDemoProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicDBProject.Data;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace AuthDemoProject.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        private readonly MusicDbContext _context;

        public SearchController(MusicDbContext dbcontext)
        {
            _context = dbcontext;
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
            ViewBag.columns = ListController.ColumnChoices;
            return View();
        }

        public IActionResult Results(string searchType, string searchTerm)
        {
            List<Song> songs;
            List<SongDetailViewModel> displaySongs = new List<SongDetailViewModel>();

            if (string.IsNullOrEmpty(searchTerm))
            {
                songs = GetAllSongsArtist();

                foreach(var song in songs)
                {
                    List<SongGenre> songGenres = FindGenresForSong(song.Id).ToList();

                    SongDetailViewModel newDisplaySong = new SongDetailViewModel(song, songGenres);
                    displaySongs.Add(newDisplaySong);
                }
            }
            else
            {
                if (searchType == "artist")
                {
                    songs = FindSongsByArtist(searchTerm).ToList();

                    foreach (Song song in songs)
                    {
                        List<SongGenre> songGenres = FindGenresForSong(song.Id).ToList();

                        SongDetailViewModel newDisplaySong = new SongDetailViewModel(song, songGenres);
                        displaySongs.Add(newDisplaySong);
                    }

                }
                else if (searchType == "genre")
                {
                    List<SongGenre> songGenres = FindSongGenresByGenre(searchTerm).ToList();

                    foreach (var song in songGenres)
                    {
                        Song foundSong = FindSongBySongGenre(song.SongId);

                        List<SongGenre> displayGenres = FindGenresForSong(foundSong.Id).ToList();

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
