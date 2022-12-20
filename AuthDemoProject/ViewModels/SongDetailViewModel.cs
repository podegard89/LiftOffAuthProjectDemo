using System;
using System.Collections.Generic;
using AuthDemoProject.Models;

namespace AuthDemoProject.ViewModels
{
	public class SongDetailViewModel
	{
		public int SongId { get; set; }
		public string Name { get; set; }
		public string ArtistName { get; set; }
		public string GenreText { get; set; }

		public SongDetailViewModel(Song theSong, List<SongGenre> songGenres)
		{
			SongId = theSong.Id;
			Name = theSong.Name;
			ArtistName = theSong.Artist.Name;

			GenreText = "";
			for (int i = 0; i < songGenres.Count; i++)
			{
				GenreText += songGenres[i].Genre.Name;
				if (i < songGenres.Count - 1)
				{
					GenreText += ", ";
				}
			}
		}
	}
}

