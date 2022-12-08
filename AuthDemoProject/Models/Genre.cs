using System;
using System.Collections.Generic;

namespace AuthDemoProject.Models
{
	public class Genre
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public List<SongGenre> SongGenres { get; set;
		}
		public Genre()
		{
		}

		public Genre(string name)
		{
			Name = name;
		}
	}
}

