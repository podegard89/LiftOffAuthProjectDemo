using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AuthDemoProject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AuthDemoProject.ViewModels
{
	public class AddSongViewModel
	{
        public string Name { get; set; }

        public int ArtistId { get; set; }

        public List<SelectListItem> Artists { get; set; }

        public List<int> GenreId { get; set; }

        public List<Genre> Genres { get; set; }

        public AddSongViewModel(List<Artist> artists, List<Genre> genres)
        {
            Artists = new List<SelectListItem>();
            foreach (var artist in artists)
            {
                Artists.Add(new SelectListItem
                {
                    Value = artist.Id.ToString(),
                    Text = artist.Name
                });
            }
            Genres = genres;
        }

        public AddSongViewModel()
        {
        }
    }

}

