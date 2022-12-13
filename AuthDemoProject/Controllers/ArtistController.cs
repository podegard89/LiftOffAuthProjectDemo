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

        public virtual IEnumerable<Artist> GetAllArtist()
        {
            return _context.Artists.Include(a => a.Song).Include(a => a.Genre).ToList();
        }

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
            IEnumerable<Artist> artist = GetAllArtist();
            return View(artist);
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
                return Redirect("/Employer");
            }
            return View("Add", addArtistViewModel);
        }

        public IActionResult About(int id)
        {
            IEnumerable<Artist> artists = GetAllArtist();
            return View();
        }

    }
}

