﻿using System;
using System.ComponentModel.DataAnnotations;

namespace AuthDemoProject.Models
{
	public class SongGenre
	{

        [Key]
        public int SongId { get; set; }
		public Song Song { get; set; }

		public int GenreId { get; set; }
		public Genre Genre { get; set; }

		public SongGenre()
		{
		}
	}
}

