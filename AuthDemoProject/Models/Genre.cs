using System;
using System.Collections.Generic;

namespace AuthDemoProject.Models
{
    public class Genre
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public List<Artist> Artists { get; set; }


        public Genre(string type)
        {
            this.Type = type;
        }

        public Genre()
        {
        }
    }
}

