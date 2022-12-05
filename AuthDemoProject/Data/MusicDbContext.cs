using AuthDemoProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthDemoProject.Data
{
    public class MusicDbContext : IdentityDbContext
    {
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public MusicDbContext(DbContextOptions<MusicDbContext> options)
            : base(options)
        {
        }


    }
}
