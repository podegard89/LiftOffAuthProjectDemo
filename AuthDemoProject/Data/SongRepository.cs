using System;
using AuthDemoProject.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AuthDemoProject.Data
{
    public interface ISongRepository
    {
        IEnumerable<Song> GetAllSongs();
        IEnumerable<Artist> GetAllArtists();
        IEnumerable<Genre> GetAllGenres();
        Song FindSongById(int id);
        void AddNewSong(Song newSong);
        void AddNewArtist(Artist newArtist);
        void SaveChanges();
        Artist FindArtistById(int id);
        IEnumerable<Artist> FindSongsForArtist(string value);
        void AddNewGenre(Genre newGenre);
        List<Artist> GetAllSongsArtist();
        IEnumerable<Song> FindSongsByArtist(string value);
        IEnumerable<Genre> FindSongsByGenre(string value);
        Artist FindArtistByGenre(int id);
        //void AddNewSkill(Skill newSkill);
        IEnumerable<Song> FindSongsByArtistAndGenre(int artistId, int genreId);
        IEnumerable<Genre> FindGenreById(int id);
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

        public List<Song> GetAllSongsArtist()
        {
            return _context.Songs
                    .Include(s => s.Artist)
                    .ToList();
        }

        public virtual IEnumerable<Artist> GetAllArtist()
        {
            return _context.Artists.ToList();
        }

        public virtual Artist FindArtistById(int id)
        {
            return _context.Artists.Find(id);
        }

        public virtual void AddNewArtist(Artist newArtist)
        {
            _context.Artists.Add(newArtist);
        }

        public virtual IEnumerable<Genre> GetAllGenres()
        {
            return _context.Genres.ToList();
        }

        public virtual void AddNewSong(Song newSong)
        {
            _context.Songs.Add(newSong);
        }

        public virtual void AddNewGenre(Genre newGenre)
        {
            _context.Genres.Add(newGenre);
        }

        public virtual void SaveChanges()
        {
            _context.SaveChanges();
        }

        public virtual Song FindSongById(int id)
        {
            return _context.Songs.Include(s => s.Artist).Single(s => s.Id == id);
        }

        /*public virtual IEnumerable<JobSkill> FindSkillsForJob(int id)
        {
            return _context.JobSkills
                .Where(js => js.JobId == id)
                .Include(js => js.Skill)
                .ToList();
        }

        public virtual IEnumerable<Job> FindJobsByEmployer(string value)
        {
            return _context.Jobs
                        .Include(j => j.Employer)
                        .Where(j => j.Employer.Name == value)
                        .ToList();
        }

        public virtual IEnumerable<JobSkill> FindJobSkillsBySkill(string value)
        {
            return _context.JobSkills
                        .Where(j => j.Skill.Name == value)
                        .Include(j => j.Job)
                        .ToList();
        }*/

        public virtual Song FindSongsByGenre(int id)
        {
            return _context.Songs
                            .Include(s => s.Genre)
                            .Single(s => s.Id == id);
        }

        /*public virtual void AddNewSkill(Skill newSkill)
        {
            _context.Skills.Add(newSkill);
        }

        public virtual IEnumerable<Song> FindSongsByArtistAndGenre(int genreId, int artistId)
        {
            return _context.Songs
                    .Where(s => s.SongId == jobId)
                    .Where(js => js.SkillId == skillId)
                    .ToList();
        }

        public virtual IEnumerable<JobSkill> FindJobSkillsById(int id)
        {
            return _context.JobSkills
                .Where(js => js.SkillId == id)
                .Include(js => js.Job)
                .Include(js => js.Skill)
                .ToList();
        }*/

        IEnumerable<Artist> ISongRepository.GetAllArtists()
        {
            throw new NotImplementedException();
        }

         Artist ISongRepository.FindArtistById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Artist> FindSongsForArtist(string value)
        {
            throw new NotImplementedException();
        }

        List<Artist> ISongRepository.GetAllSongsArtist()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Song> FindSongsByArtist(string value)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Genre> FindSongsByGenre(string value)
        {
            throw new NotImplementedException();
        }

        public Artist FindArtistByGenre(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Song> FindSongsByArtistAndGenre(int artistId, int genreId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Genre> FindGenreById(int id)
        {
            throw new NotImplementedException();
        }
    }
}

