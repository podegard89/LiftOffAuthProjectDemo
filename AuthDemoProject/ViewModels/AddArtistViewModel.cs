using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AuthDemoProject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AuthDemoProject.ViewModels
{
	public class AddArtistViewModel
	{
        [Required(ErrorMessage = "Please enter a valid artist name")]
        public string Name { get; set; }

		public string Genre { get; set; }

		public Song Song { get; set; }

		public List<SelectListItem> Artists { get; set; }
		public AddArtistViewModel(Song theSong, List<Artist> possibleArtists)
		{
			Artists = new List<SelectListItem>();

			foreach (var artist in possibleArtists)
			{
				Artists.Add(new SelectListItem
				{
					Value = artist.Id.ToString(),
					Text = artist.Name
				});
			}

			Song = theSong;
		}

		public AddArtistViewModel()
		{
		}
	}
}

