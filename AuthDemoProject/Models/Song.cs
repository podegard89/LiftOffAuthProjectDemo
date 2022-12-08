using System;
namespace AuthDemoProject.Models
{
    public class Song
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Genre { get; set; }

        public string Artist { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }

        public Song(string name, string genre, string artist)
        {
            this.Name = name;
            this.Genre = genre;
            this.Artist = artist;
        }

        public Song()
        {
        }
    }
}

