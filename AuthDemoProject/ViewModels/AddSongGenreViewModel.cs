using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AuthDemoProject.Models;

namespace AuthDemoProject.ViewModels
{
    public class AddSongGenreViewModel
    {
        [Required(ErrorMessage = "Song is required")]
        public int SongId { get; set; }

        [Required(ErrorMessage = "Genre is required")]
        public int GenreId { get; set; }

        public Song Song { get; set; }

        public List<SelectListItem> Genres { get; set; }

        public AddSongGenreViewModel(Song theSong, List<Genre> possibleGenres)
        {
            Genres = new List<SelectListItem>();

            foreach (var genre in possibleGenres)
            {
                Genres.Add(new SelectListItem
                {
                    Value = genre.Id.ToString(),
                    Text = genre.Name
                });
            }

            Song = theSong;
        }

        public AddSongGenreViewModel()
        {
        }

    }
}
