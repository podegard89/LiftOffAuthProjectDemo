using System;
using System.Collections.Generic;

namespace AuthDemoProject.Models
{
    public class Artist
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Genre { get; set; }

        public List<Song> Song { get; set; }


        public Artist()
        {
        }

        public Artist(string name, string genre)
        {
            this.Name = name;
            this.Genre = genre;
        }

        public Artist(string name)
        {
            this.Name = name;
        }
    }
}

