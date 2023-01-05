using System;
using AuthDemoProject.Models;
using Microsoft.AspNetCore.Mvc;
using MusicDBProject.Data;
using System.Collections.Generic;
using AuthDemoProject.ViewModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MusicDBProject.Controllers
{
    public class SongController : Controller
    {
        private readonly MusicDbContext _context;

        public SongController(MusicDbContext dbcontext)
        {
            _context = dbcontext;
        }

        //Repo Method
        public virtual IEnumerable<Song> GetAllSongs()
        {
            return _context.Songs.ToList();
        }

        //Repo Method - added in
        public List<Song> GetAllSongsArtist()
        {
            return _context.Songs
                .Include(j => j.Artist)
                .ToList();
        }

        //Repo Method - added in
        public virtual IEnumerable<Song> FindSongsByArtist(string value)
        {
            return _context.Songs
                .Include(s => s.Artist)
                .Where(s => s.Artist.Name == value)
                .ToList();
        }

        //Repo Method - added in
        public virtual Song FindSongBySongGenre(int id)
        {
            return _context.Songs.Include(s => s.Artist).Single(s => s.Id == id);
        }

        public virtual void AddNewSong(Song newSong)
        {
            _context.Songs.Add(newSong);
        }

        public virtual void SaveChanges()
        {
            _context.SaveChanges();
        }

        //Repo Method
        public virtual Song FindSongById(int id)
        {
            return _context.Songs.Include(s => s.Artist).Single(s => s.Id == id);
            //return _context.Songs.Find(id);
        }

        //Repo Method
        public virtual Artist FindArtistById(int id)
        {
            return _context.Artists.Find(id);
        }
        public virtual IEnumerable<Artist> GetAllArtists()
        {
            return _context.Artists.ToList();
        }
        //Repo Method
        public virtual IEnumerable<Genre> GetAllGenres()
        {
            return _context.Genres.ToList();
        }

        public virtual IEnumerable<SongGenre> FindGenresForSong(int id)
        {
            return _context.SongGenres
                .Where(sg => sg.SongId == id)
                .Include(sg => sg.Genre)
                .ToList();
        }

        public virtual void AddNewSongGenre(SongGenre newSongGenre)
        {
            _context.SongGenres.Add(newSongGenre);
        }


        public IActionResult Index()
        {
            IEnumerable<Song> songs = GetAllSongs();
            return View(songs);
        }

        public IActionResult AddSong()
        {
            AddSongViewModel addSongViewModel = new AddSongViewModel(GetAllArtists().ToList(), GetAllGenres().ToList());
            return View(addSongViewModel);
        }


        //[HttpPost]
        //[Route(("Song/Add"))]
        //public IActionResult AddSong(AddSongViewModel addSongViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Song newSong = new Song
        //        {
        //            Name = addSongViewModel.Name,
        //            Genre = addSongViewModel.Genre
        //        };
        //        AddNewSong(newSong);
        //        SaveChanges();
        //        return Redirect("/Song");
        //    }

        //    return View("Add", addSongViewModel);
        //}

        public IActionResult ProcessAddSongForm(AddSongViewModel addSongViewModel, string[] selectedGenres)
        {
            if (ModelState.IsValid)
            {
                Song song = new Song
                {
                    Name = addSongViewModel.Name,
                    ArtistId = addSongViewModel.ArtistId,
                    Artist = FindArtistById(addSongViewModel.ArtistId)
                    //Genre = addSongViewModel.Genres.ToString()
                };
                for (int i = 0; i < selectedGenres.Length; i++)
                {
                    SongGenre newSongGenre = new SongGenre
                    {
                        SongId = song.Id,
                        Song = song,
                        GenreId = Int32.Parse(selectedGenres[i])  //trying different loop? 
                    };
                    AddNewSongGenre(newSongGenre);
                }
                AddNewSong(song);
                SaveChanges();
                return Redirect("Index");
            }
            return View("Add", addSongViewModel);
        }

        //TODO - Check Delete methods/  Write EDIT Methods

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Song theSong = FindSongById(id);
            List<SongGenre> songGenres = FindGenresForSong(id).ToList();
            SongDetailViewModel viewModel = new SongDetailViewModel(theSong, songGenres);
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult SubmitEditSongForm(SongDetailViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Song theSong = FindSongById(viewModel.SongId);
                theSong.Name = viewModel.Name;
                theSong.Artist.Name = viewModel.ArtistName;
                //theSong.GenreText = viewModel.GenreText;
                SaveChanges();
                return Redirect("/Song");
            }
            return View(viewModel);
        }

        public IActionResult Delete()
        {
            ViewBag.songs = _context.Songs.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Delete(int[] songIds)
        {
            foreach (int songId in songIds)
            {
                Song theSong = _context.Songs.Find(songId);
                _context.Songs.Remove(theSong);
            }
            _context.SaveChanges();

            return Redirect("/Song");
        }

        public IActionResult About(int id)
        {
            IEnumerable<Song> songs = GetAllSongs();
            return View(songs);
        }

        public IActionResult Detail(int id)
        {
            Song theSong = FindSongById(id);

            List<SongGenre> songGenres = FindGenresForSong(id).ToList();

            SongDetailViewModel viewModel = new SongDetailViewModel(theSong, songGenres);
            return View(viewModel);
        }


    }
}

