using System;
using System.ComponentModel.DataAnnotations;

namespace AuthDemoProject.ViewModels
{
	public class AddSongViewModel
	{
		[Required(ErrorMessage ="Please enter a valid name")]
		public string Name { get; set; }

		public string Genre { get; set; }

		public string Artist { get; set; }

        public AddSongViewModel()
        {

        }
    }

}

