using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AuthDemoProject.Models
{
    public class Song
    {
       
        public int Id { get; set; }

        public string Name { get; set; }

        public string Genre { get; set; }

        public Artist Artist { get; set; }

        public int ArtistId { get; set; }

        public List<SongGenre> SongGenres { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }

        public Song()
        {
        }
        public Song(string name)
        {
            Name = name;
        }
    }
}

