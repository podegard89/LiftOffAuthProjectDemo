using System;
using AuthDemoProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MusicDBProject.Data
{
    public class MusicDbContext : IdentityDbContext
    {
        public DbSet<Song> Songs { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<SongGenre> SongGenres { get; set; }

        public MusicDbContext(DbContextOptions<MusicDbContext> options) : base(options)
        {
        }


    }
}

