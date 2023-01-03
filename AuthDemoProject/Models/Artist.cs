using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AuthDemoProject.Models
{
	public class Artist
	{

        public int Id { get; set; }

		public string Name { get; set; }

		public string Genre { get; set; }

		public List<Song> Songs { get; set; }

        public Artist()
        {

        }
        public Artist(string name, string genre)
		{
			Name = name;
			Genre = genre;
		}

	}
}

