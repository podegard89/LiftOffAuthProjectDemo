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
	public class ArtistController : Controller
	{
		private readonly MusicDbContext _context;

		public ArtistController(MusicDbContext dbcontext)
		{
			_context = dbcontext;
		}

        //Repo Method - add s to GetAllArtist() - make plural (& line39)
        public virtual IEnumerable<Artist> GetAllArtists()
        {
            return _context.Artists.ToList();
        }

        //Repo Method
        public virtual Artist FindArtistById(int id)
        {
            return _context.Artists.Find(id);
        }

        //Repo Method
        public virtual void AddNewArtist(Artist newArtist)
        {
            _context.Artists.Add(newArtist);
        }

        public virtual void SaveChanges()
        {
            _context.SaveChanges();
        }

        //GET: /<controller>/
        public IActionResult Index()
        {
            IEnumerable<Artist> artists = GetAllArtists();
            return View(artists);
        }

        public IActionResult Add()
        {
            AddArtistViewModel addArtistViewModel = new AddArtistViewModel();
            return View(addArtistViewModel);
        }

        public IActionResult ProcessAddArtistForm(AddArtistViewModel addArtistViewModel)
        {
            if (ModelState.IsValid)
            {
                Artist artist = new Artist
                {
                    Name = addArtistViewModel.Name
                };
                AddNewArtist(artist);
                SaveChanges();
                return Redirect("/Artist");
            }
            return View("Add", addArtistViewModel);
        }

        //TODO CHECK THE DELETE METHODS
        public IActionResult Delete()
        {
            ViewBag.songs = _context.Artists.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Delete(int[] artistIds)
        {
            foreach (int artistId in artistIds)
            {
                Artist theArtist = _context.Artists.Find(artistId);
                _context.Artists.Remove(theArtist);
            }
            _context.SaveChanges();

            return Redirect("/Artist");
        }


        public IActionResult About(int id)
        {
            Artist artist = FindArtistById(id);
            return View(artist);
        }

    }
}

