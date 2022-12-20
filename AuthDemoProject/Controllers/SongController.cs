﻿using System;
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

        public virtual IEnumerable<Song> GetAllSongs()
        {
            return _context.Songs.ToList();
        }

        public virtual void AddNewSong(Song newSong)
        {
            _context.Songs.Add(newSong);
        }

        public virtual void SaveChanges()
        {
            _context.SaveChanges();
        }

        public virtual Song FindSongById(int id)
        {
            return _context.Songs.Find(id);
        }

        public virtual IEnumerable<SongGenre> FindGenresForSong(int id)
        {
            return _context.SongGenres
                .Where(sg => sg.SongId == id)
                .Include(sg => sg.Genre)
                .ToList();
        }

        public IActionResult Index()
        {
            IEnumerable<Song> songs = GetAllSongs();
            return View(songs);
        }

        [HttpGet("/Add")]
        public IActionResult Add()
        {
            AddSongViewModel addSongViewModel = new AddSongViewModel();
            return View(addSongViewModel);
        }

        

        [HttpPost("Song/Add")]
        public IActionResult AddSong(AddSongViewModel addSongViewModel)
        {
            if (ModelState.IsValid)
            {
                Song newSong = new Song
                {
                    Name = addSongViewModel.Name,
                    Genre = addSongViewModel.Genre
                };
                AddNewSong(newSong);
                SaveChanges();
                return Redirect("/Song");
            }

            return View("Add", addSongViewModel);
        }

        public IActionResult ProcessAddSongForm(AddSongViewModel addSongViewModel)
        {
            if (ModelState.IsValid)
            {
                Song song = new Song
                {
                    Name = addSongViewModel.Name,
                    Genre = addSongViewModel.Genre
                };
                AddNewSong(song);
                SaveChanges();
                return Redirect("/Song");
            }
            return View("Add");
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

