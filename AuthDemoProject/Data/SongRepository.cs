using System;
using System.Collections.Generic;
using System.Linq;
using AuthDemoProject.Models;
using Microsoft.EntityFrameworkCore;

namespace MusicDBProject.Data
{
    public interface ISongRepository
    {
        IEnumerable<Song> GetAllSongs();
        void AddNewSong(Song newSong);
        Song FindSongById(int id);
        void SaveChanges();

        IEnumerable<Artist> GetAllArtist();
        void AddNewArtist(Artist newArtist);
        Artist FindArtistById(int id);

        IEnumerable<Genre> GetAllGenres();
        void AddNewGenre(Genre newGenre);
        Genre FindGenreById(int id);
        void AddNewSongGenre(SongGenre newSongGenre);
        IEnumerable<SongGenre> FindGenresForSong(int id);

        List<Song> GetAllSongsArtist();
        IEnumerable<Song> FindSongsByArtist(string value);
        IEnumerable<SongGenre> FindSongGenresByGenre(string value);
        Song FindSongBySongGenre(int id);
        IEnumerable<SongGenre> FindSongsGenresByGenreAndSong(int songId, int genreId);
        IEnumerable<SongGenre> FindSongGenresById(int id);

    }

    public class SongRepository : ISongRepository
    {
        private readonly MusicDbContext _context;

        public SongRepository()
        {
        }

        public SongRepository(MusicDbContext context)
        {
            _context = context;
        }

        public virtual IEnumerable<Song> GetAllSongs()
        {
            return _context.Songs.Include(s => s.Artist).Include(s => s.Genre).ToList();
        }

        public virtual void AddNewSong(Song newSong)
        {
            _context.Songs.Add(newSong);
        }

        public virtual Song FindSongById(int id)
        {
            return _context.Songs.Find(id);
        }


        public virtual IEnumerable<Artist> GetAllArtist()
        {
            return _context.Artists.Include(a => a.Song).Include(a => a.Genre).ToList();
        }

        public virtual void AddNewArtist(Artist newArtist)
        {
            _context.Artists.Add(newArtist);
        }

        public virtual Artist FindArtistById(int id)
        {
            return _context.Artists.Find(id);
        }

        public virtual IEnumerable<Genre> GetAllGenres()
        {
            return _context.Genres.ToList();
        }

        public virtual void AddNewGenre(Genre newGenre)
        {
            _context.Genres.Add(newGenre);
        }

        public virtual Genre FindGenreById(int id)
        {
            return _context.Genres.Find(id);
        }

        public virtual void AddNewSongGenre(SongGenre newSongGenre)
        {
            _context.SongGenres.Add(newSongGenre);
        }

        public virtual IEnumerable<SongGenre> FindGenresForSong(int id)
        {
            return _context.SongGenres
                .Where(sg => sg.SongId == id)
                .Include(sg => sg.Genre)
                .ToList();
        }

        public List<Song> GetAllSongsArtist()
        {
            return _context.Songs
                .Include(s => s.Artist)
                .ToList();
        }

        public virtual IEnumerable<Song> FindSongsByArtist(string value)
        {
            return _context.Songs
                .Include(s => s.Artist)
                .Where(s => s.Artist.Name == value)
                .ToList();
        }

        public virtual IEnumerable<SongGenre> FindSongGenresByGenre(string value)
        {
            return _context.SongGenres
                .Where(s => s.Genre.Name == value)
                .Include(s => s.Song)
                .ToList();
        }

        public virtual Song FindSongBySongGenre(int id)
        {
            return _context.Songs
                .Include(s => s.Artist)
                .Single(s => s.Id == id);
        }

        public virtual IEnumerable<SongGenre> FindSongsGenresByGenreAndSong(int songId, int genreId)
        {
            return _context.SongGenres
                .Where(sg => sg.SongId == songId)
                .Where(sg => sg.GenreId == genreId)
                .ToList();
        }

        public virtual IEnumerable<SongGenre> FindSongGenresById(int id)
        {
            return _context.SongGenres
                .Where(sg => sg.GenreId == id)
                .Include(sg => sg.Song)
                .Include(sg => sg.Genre)
                .ToList();
        }
        public virtual void SaveChanges()
        {
            _context.SaveChanges();
        }

    }
}
