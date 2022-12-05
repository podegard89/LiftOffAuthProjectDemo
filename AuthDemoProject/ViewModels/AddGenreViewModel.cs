using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AuthDemoProject.Models;

namespace AuthDemoProject.ViewModels
{
	public class AddGenreViewModel
	{
        [Required(ErrorMessage = "Please enter a type of genre")]
		public string Type { get; set; }

		public List<Artist> Artist { get; set; }

        public AddGenreViewModel()
		{
		}
	}
}

