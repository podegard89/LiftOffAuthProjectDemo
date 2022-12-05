using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AuthDemoProject.Models;

namespace AuthDemoProject.ViewModels
{
	public class AddArtistViewModel
	{
        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }

        public string Genre { get; set; }

        public List<Song> Song { get; set; }

        public AddArtistViewModel()
		{
		}
	}
}

